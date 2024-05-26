using BaggageTrackerApi.Entities.DTOs;
using BaggageTrackerApi.Enums;

namespace BaggageTrackerApi.Models;

public class QrCodeScanResponse(BaggageDto? baggage, UserDto? user, QrCodeScanResult scanResult)
{
    public BaggageDto? Baggage { get; set; } = baggage;

    public UserDto? User { get; set; } = user;

    public QrCodeScanResult ScanResult { get; set; } = scanResult;
}