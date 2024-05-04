using BaggageTrackerApi.Enums;

namespace BaggageTrackerApi.Entities.DTOs;

public class UserDto
{
    public long Id { get; set; }
    
    public string Name { get; set; }
    
    public string Surname { get; set; }
    
    public UserRole Role { get; init; }
    
    public FlightDto ActiveFlight { get; set; }
    
    public IEnumerable<BaggageDto> Baggages { get; set; }
}