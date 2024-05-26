using BaggageTrackerApi.Enums;

namespace BaggageTrackerApi.Exceptions;

public class PersonnelCanNotQueryBaggageStatusException() : ApiDomainException($"{nameof(UserRole.Personnel)} can't query baggage status.");