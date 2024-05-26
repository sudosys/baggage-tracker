namespace BaggageTrackerApi.Exceptions;

public class BaggageDoesNotExistException(string baggageId) : ApiDomainException($"Baggage with id '{baggageId}' does not exist");