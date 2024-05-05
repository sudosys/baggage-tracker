namespace BaggageTrackerApi.Services;

public class BaggageTrackingService(UserService userService, QrCodeGenerationService qrCodeGenerationService)
{
    public MemoryStream GenerateQrCodesForFlight(string flightNumber)
    {
        var users = userService.GetUsersByFlightNumber(flightNumber);
        
        var qrCodes = qrCodeGenerationService.CreateQrCodes(users);
        var compressed = QrCodeGenerationService.CompressQrCodes(qrCodes);
        
        compressed.Seek(0, SeekOrigin.Begin);

        return compressed;
    }
}