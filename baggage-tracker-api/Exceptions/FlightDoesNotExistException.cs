namespace BaggageTrackerApi.Exceptions;

public class FlightDoesNotExistException(string flightNumber) : ApiDomainException($"Flight {flightNumber} does not exist.");