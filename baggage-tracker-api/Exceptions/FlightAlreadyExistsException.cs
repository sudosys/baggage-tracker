namespace BaggageTrackerApi.Exceptions;

public class FlightAlreadyExistsException(string flightNumber) : ApiDomainException($"Flight {flightNumber} is already registered.");