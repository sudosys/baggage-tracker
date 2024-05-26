namespace BaggageTrackerApi.Exceptions;

public class BaggageNotOwnedByPassengerException() : ApiDomainException("Baggage not owned by the passenger");