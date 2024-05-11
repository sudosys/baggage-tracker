namespace BaggageTrackerApi.Enums;

public enum QrCodeScanResult
{
    Success,
    CodeInvalid,
    NotOwnedByPassenger,
    UnknownError
}