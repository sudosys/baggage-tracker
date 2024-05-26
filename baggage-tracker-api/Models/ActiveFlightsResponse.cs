namespace BaggageTrackerApi.Models;

public class ActiveFlightsResponse(List<ActiveFlight> activeFlights)
{
    public List<ActiveFlight> ActiveFlights { get; set; } = activeFlights;
}