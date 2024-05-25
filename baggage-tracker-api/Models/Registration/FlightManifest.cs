namespace BaggageTrackerApi.Models.Registration;

public class FlightManifest(string flightNumber, IEnumerable<PassengerRegistration> passengers)
{
    public string FlightNumber { get; set; } = flightNumber;

    public IEnumerable<PassengerRegistration> Passengers { get; set; } = passengers;
}