using BaggageTrackerApi.Enums;

namespace BaggageTrackerApi.Exceptions;

public class BaggageAlreadyMarkedWithSameStatusException(string baggageId, BaggageStatus status) 
    : ApiDomainException($"Baggage with id '{baggageId}' already marked as '{status}'");