using AutoMapper;
using BaggageTrackerApi.Entities;
using BaggageTrackerApi.Entities.DTOs;
using BaggageTrackerApi.Enums;
using BaggageTrackerApi.Models;

namespace BaggageTrackerApi.Services;

public class BaggageTrackingService(
    BaggageTrackerDbContext baggageTrackerDbContext,
    UserService userService,
    QrCodeGenerationService qrCodeGenerationService,
    UbcProcessor ubcProcessor,
    IMapper mapper)
{
    private static readonly BaggageStatus[] PassengerAllowedStatuses = [BaggageStatus.ReceivedByThePassenger];
    private static readonly BaggageStatus[] PersonnelAllowedStatuses = [
        BaggageStatus.WaitingForLoad,
        BaggageStatus.InThePlane,
        BaggageStatus.UnloadedFromThePlane,
        BaggageStatus.InTheLostOffice];
    
    public MemoryStream GenerateQrCodesForFlight(string flightNumber)
    {
        var users = userService.GetUsersByFlightNumber(flightNumber);
        
        var qrCodes = qrCodeGenerationService.CreateQrCodes(users);
        var compressed = QrCodeGenerationService.CompressQrCodes(qrCodes);
        
        compressed.Seek(0, SeekOrigin.Begin);

        return compressed;
    }

    public List<BaggageDto> GetBaggageStatus(long userId)
    {
        var user = userService.GetUserById(userId);

        if (user == null)
        {
            throw new Exception($"User with id {userId} does not exist");
        }
        
        if (user.Role == UserRole.Personnel)
        {
            throw new Exception($"{nameof(UserRole.Personnel)} can't query baggage status.");
        }

        return baggageTrackerDbContext.Baggages
            .Where(b => b.UserId == userId)
            .AsEnumerable()
            .Select(mapper.Map<BaggageDto>)
            .ToList();
    }
    
    public void SetBaggageStatus(UserDto user, Guid baggageId, BaggageStatus newStatus)
    {
        ValidateBaggageStatusByRole(user, newStatus);

        var isBaggageOwner = IsBaggageOwner(user.Id, baggageId) != null;
        if (user is { Role: UserRole.Passenger } && !isBaggageOwner)
        {
            throw new Exception("Baggage not owned by the passenger");
        }
        
        var baggage = baggageTrackerDbContext.Baggages
            .FirstOrDefault(b => b.BaggageId == baggageId);

        if (baggage == null)
        {
            throw new Exception($"Baggage with id '{baggageId}' does not exist");
        }

        if (baggage.BaggageStatus == newStatus)
        {
            throw new Exception($"Baggage with id '{baggageId}' already marked as '{newStatus}'");
        }

        baggage.BaggageStatus = newStatus;
        
        baggageTrackerDbContext.SaveChanges();
    }

    private static void ValidateBaggageStatusByRole(UserDto user, BaggageStatus newStatus)
    {
        if (user.Role == UserRole.Passenger && !PassengerAllowedStatuses.Contains(newStatus))
        {
            throw new Exception(
                $"Passenger can't set a baggage status other than '{string.Join(',', PassengerAllowedStatuses)}'");
        }
        
        if (user.Role == UserRole.Personnel && !PersonnelAllowedStatuses.Contains(newStatus))
        {
            throw new Exception(
                $"Personnel can't set the baggage status '{newStatus}'");
        }
    }

    public QrCodeScanResponse ProcessQrCodeScan(UserDto requestedUser, string qrCodeData)
    {
        var ubc = ubcProcessor.ParseUbc(qrCodeData);

        if (!ubcProcessor.ValidateUbc(ubc))
        {
            return new QrCodeScanResponse(null, QrCodeScanResult.CodeInvalid);
        }

        var user = baggageTrackerDbContext.Users.SingleOrDefault(u => u.Id == ubc.UserId);
        var baggage = IsBaggageOwner(user?.Id, ubc.BaggageId); 
        
        if (baggage != null && (requestedUser.Id == user?.Id 
                               || requestedUser is { Role: UserRole.Personnel }))
        {
            return new QrCodeScanResponse(baggage.BaggageId.ToString(), QrCodeScanResult.Success);
        }

        return new QrCodeScanResponse(null, QrCodeScanResult.NotOwnedByPassenger);
    }
    
    private Baggage? IsBaggageOwner(long? userId, Guid baggageId) => 
        baggageTrackerDbContext.Baggages.SingleOrDefault(b => b.UserId == userId && b.BaggageId == baggageId);
}