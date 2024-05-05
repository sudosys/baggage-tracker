namespace BaggageTrackerApi.Models.QrCode;

public class UniqueBaggageCode(string flightNumber, string username, Guid baggageId)
{
    public string FlightNumber { get; init; } = flightNumber;
    
    public string Username { get; init; } = username;
    
    public Guid BaggageId { get; init; } = baggageId;
}