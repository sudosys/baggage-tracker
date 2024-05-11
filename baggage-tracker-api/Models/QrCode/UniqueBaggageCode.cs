namespace BaggageTrackerApi.Models.QrCode;

public class UniqueBaggageCode(string flightNumber, long userId, Guid baggageId)
{
    public string FlightNumber { get; init; } = flightNumber;
    
    public long UserId { get; init; } = userId;
    
    public Guid BaggageId { get; init; } = baggageId;
}