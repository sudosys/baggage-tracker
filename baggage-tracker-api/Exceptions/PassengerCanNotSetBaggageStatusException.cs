using BaggageTrackerApi.Services;

namespace BaggageTrackerApi.Exceptions;

public class PassengerCanNotSetBaggageStatusException() : 
    ApiDomainException($"Passenger can't set a baggage status other than '{string.Join(',', BaggageTrackingService.PassengerAllowedStatuses)}'");