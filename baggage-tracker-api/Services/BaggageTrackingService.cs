using AutoMapper;
using BaggageTrackerApi.Entities;
using BaggageTrackerApi.Entities.DTOs;
using BaggageTrackerApi.Enums;
using BaggageTrackerApi.Exceptions;
using BaggageTrackerApi.Extensions;
using BaggageTrackerApi.Models;

namespace BaggageTrackerApi.Services;

public class BaggageTrackingService(
    BaggageTrackerDbContext baggageTrackerDbContext,
    UserService userService,
    QrCodeGenerationService qrCodeGenerationService,
    UbcProcessor ubcProcessor,
    IMapper mapper)
{
    public static readonly BaggageStatus[] PassengerAllowedStatuses = [BaggageStatus.ReceivedByThePassenger];
    public static readonly BaggageStatus[] PersonnelAllowedStatuses = [
        BaggageStatus.WaitingForLoad,
        BaggageStatus.InThePlane,
        BaggageStatus.UnloadedFromThePlane,
        BaggageStatus.InTheLostOffice];
    
    public MemoryStream GenerateQrCodesForFlight(string flightNumber)
    {
        var users = GetPassengersByFlightNumber(flightNumber);
        
        var qrCodes = qrCodeGenerationService.CreateQrCodes(users);
        var compressed = QrCodeGenerationService.CompressQrCodes(qrCodes);
        
        compressed.Seek(0, SeekOrigin.Begin);

        return compressed;
    }
    
    public IEnumerable<UserDto> GetPassengersByFlightNumber(string flightNumber)
    {
        if (!baggageTrackerDbContext.DoesFlightExist(flightNumber))
        {
            throw new FlightDoesNotExistException(flightNumber);
        }

        var usersByFlightNumber = baggageTrackerDbContext.Users
            .QueryUserWithFlightData()
            .Where(u => u.ActiveFlight != null &&
                        u.ActiveFlight.FlightNumber == flightNumber &&
                        u.Role == UserRole.Passenger)
            .AsEnumerable()
            .Select(mapper.Map<UserDto>)
            .ToList();

        return usersByFlightNumber;
    }

    public BaggageInfoResponse GetBaggageStatus(long userId)
    {
        var user = userService.GetUserById(userId);

        if (user == null)
        {
            throw new UserDoesNotExistException(userId);
        }
        
        if (user.Role == UserRole.Personnel)
        {
            throw new PersonnelCanNotQueryBaggageStatusException();
        }

        var flightNumber = user.ActiveFlight?.FlightNumber;

        if (flightNumber == null)
        {
            throw new ActiveFlightCouldNotBeFoundException();
        }

        var baggages = baggageTrackerDbContext.Baggages
            .Where(b => b.UserId == userId)
            .AsEnumerable()
            .Select(mapper.Map<BaggageDto>)
            .ToList();

        return new BaggageInfoResponse(flightNumber, baggages);
    }
    
    public void SetBaggageStatus(UserDto user, Guid baggageId, BaggageStatus newStatus)
    {
        ValidateBaggageStatusByRole(user, newStatus);

        var isBaggageOwner = IsBaggageOwner(user.Id, baggageId) != null;
        if (user is { Role: UserRole.Passenger } && !isBaggageOwner)
        {
            throw new BaggageNotOwnedByPassengerException();
        }
        
        var baggage = baggageTrackerDbContext.Baggages
            .FirstOrDefault(b => b.BaggageId == baggageId);

        if (baggage == null)
        {
            throw new BaggageDoesNotExistException(baggageId.ToString());
        }

        if (baggage.BaggageStatus == newStatus)
        {
            throw new BaggageAlreadyMarkedWithSameStatusException(baggageId.ToString(), newStatus);
        }

        baggage.BaggageStatus = newStatus;
        
        baggageTrackerDbContext.SaveChanges();
    }

    private static void ValidateBaggageStatusByRole(UserDto user, BaggageStatus newStatus)
    {
        if (user.Role == UserRole.Passenger && !PassengerAllowedStatuses.Contains(newStatus))
        {
            throw new PassengerCanNotSetBaggageStatusException();
        }
        
        if (user.Role == UserRole.Personnel && !PersonnelAllowedStatuses.Contains(newStatus))
        {
            throw new PersonnelCanNotSetBaggageStatusException(newStatus);
        }
    }

    public QrCodeScanResponse ProcessQrCodeScan(UserDto requestedUser, string qrCodeData)
    {
        var ubc = ubcProcessor.ParseUbc(qrCodeData);

        if (!ubcProcessor.ValidateUbc(ubc))
        {
            return new QrCodeScanResponse(null, null, QrCodeScanResult.CodeInvalid);
        }

        var user = baggageTrackerDbContext.Users.SingleOrDefault(u => u.Id == ubc.UserId);
        var baggage = IsBaggageOwner(user?.Id, ubc.BaggageId); 
        
        if (baggage != null && (requestedUser.Id == user?.Id 
                               || requestedUser is { Role: UserRole.Personnel }))
        {
            return new QrCodeScanResponse(
                mapper.Map<BaggageDto>(baggage),
                mapper.Map<UserDto>(user),
                QrCodeScanResult.Success);
        }

        return new QrCodeScanResponse(null, null, QrCodeScanResult.NotOwnedByPassenger);
    }
    
    private Baggage? IsBaggageOwner(long? userId, Guid baggageId) => 
        baggageTrackerDbContext.Baggages.SingleOrDefault(b => b.UserId == userId && b.BaggageId == baggageId);
}