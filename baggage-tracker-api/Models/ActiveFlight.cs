namespace BaggageTrackerApi.Models;

public class ActiveFlight(string flightNumber, int passengerCount)
{
    public string FlightNumber { get; set; } = flightNumber;

    public int PassengerCount { get; set; } = passengerCount;
}