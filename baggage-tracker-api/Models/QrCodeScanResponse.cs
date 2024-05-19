using BaggageTrackerApi.Enums;

namespace BaggageTrackerApi.Models;

public class QrCodeScanResponse(string? baggageId, QrCodeScanResult scanResult)
{
    public string? BaggageId { get; set; } = baggageId;

    public QrCodeScanResult ScanResult { get; set; } = scanResult;
}