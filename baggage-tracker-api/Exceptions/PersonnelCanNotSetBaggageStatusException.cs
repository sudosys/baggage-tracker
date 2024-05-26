using BaggageTrackerApi.Enums;
using BaggageTrackerApi.Services;

namespace BaggageTrackerApi.Exceptions;

public class PersonnelCanNotSetBaggageStatusException(BaggageStatus status) : ApiDomainException($"Personnel can't set the baggage status '{status}'");