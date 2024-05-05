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
}