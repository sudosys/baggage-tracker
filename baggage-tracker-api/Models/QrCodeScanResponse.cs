using BaggageTrackerApi.Entities.DTOs;
using BaggageTrackerApi.Enums;

namespace BaggageTrackerApi.Models;

public class QrCodeScanResponse(BaggageDto? baggage, QrCodeScanResult scanResult)
{
    public BaggageDto? Baggage { get; set; } = baggage;

    public QrCodeScanResult ScanResult { get; set; } = scanResult;
}