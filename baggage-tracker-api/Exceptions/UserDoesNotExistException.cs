namespace BaggageTrackerApi.Exceptions;

public class UserDoesNotExistException(long userId) : ApiDomainException($"User with id {userId} does not exist");