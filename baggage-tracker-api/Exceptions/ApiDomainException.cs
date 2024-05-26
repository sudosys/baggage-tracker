namespace BaggageTrackerApi.Exceptions;

public abstract class ApiDomainException(string message) : Exception(message);