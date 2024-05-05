using AutoMapper;
using BaggageTrackerApi.Entities.DTOs;
using BaggageTrackerApi.Enums;

namespace BaggageTrackerApi.Services;

public class BaggageTrackingService(
    BaggageTrackerDbContext baggageTrackerDbContext,
    UserService userService,
    QrCodeGenerationService qrCodeGenerationService,
    IMapper mapper)
{
    private static BaggageStatus[] _passengerAllowedStatuses = [BaggageStatus.ReceivedByThePassenger];
    private static BaggageStatus[] _personnelAllowedStatuses = [
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
        
        if (user is { Role: UserRole.Passenger } && !IsBaggageOwner(user.Id, baggageId))
        {
            throw new Exception("Baggage not owned by the passenger");
        }
        
        var baggage = baggageTrackerDbContext.Baggages
            .FirstOrDefault(b => b.BaggageId == baggageId);

        if (baggage == null)
        {
            throw new Exception($"Baggage with id {baggageId} does not exist");
        }

        baggage.BaggageStatus = newStatus;
        
        baggageTrackerDbContext.SaveChanges();
    }

    private static void ValidateBaggageStatusByRole(UserDto user, BaggageStatus newStatus)
    {
        if (user.Role == UserRole.Passenger && !_passengerAllowedStatuses.Contains(newStatus))
        {
            throw new Exception(
                $"Passenger can't set a baggage status other than '{newStatus}'");
        }
        
        if (user.Role == UserRole.Personnel && !_personnelAllowedStatuses.Contains(newStatus))
        {
            throw new Exception(
                $"Personnel can't set the baggage status '{newStatus}'");
        }
    }

    private bool IsBaggageOwner(long userId, Guid baggageId) => 
        baggageTrackerDbContext.Baggages.Any(b => b.UserId == userId && b.BaggageId == baggageId);
}