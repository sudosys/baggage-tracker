namespace BaggageTrackerApi.Exceptions;

public class FlightAlreadyExistsException(string message) : Exception(message);