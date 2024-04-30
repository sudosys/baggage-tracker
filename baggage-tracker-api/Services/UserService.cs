using BaggageTrackerApi.Entities;
using BaggageTrackerApi.Entities.DTOs;

namespace BaggageTrackerApi.Services;

public class UserService
{
    public UserDto ConvertToDto(User user) => new()
    {
        Id = user.Id,
        Name = user.Name,
        Surname = user.Surname,
        ActiveFlight = new FlightDto
        {
            Id = user.ActiveFlight.Id,
            FlightNumber = user.ActiveFlight.FlightNumber
        },
        Baggages = user.Baggages.Select(b => new BaggageDto
        {
            Id = b.Id,
            TagNumber = b.TagNumber
        })
    };
}