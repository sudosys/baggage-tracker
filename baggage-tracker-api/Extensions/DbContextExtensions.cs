namespace BaggageTrackerApi.Extensions;

public static class DbContextExtensions
{
    public static bool DoesFlightExist(this BaggageTrackerDbContext baggageTrackerDbContext, string flightNumber) => 
        baggageTrackerDbContext.Flights.Any(f => f.FlightNumber == flightNumber);
}