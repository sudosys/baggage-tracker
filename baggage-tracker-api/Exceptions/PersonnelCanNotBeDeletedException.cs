using BaggageTrackerApi.Enums;

namespace BaggageTrackerApi.Exceptions;

public class PersonnelCanNotBeDeletedException() : 
    ApiDomainException($"Users with the role of {nameof(UserRole.Personnel)} can't be deleted.");