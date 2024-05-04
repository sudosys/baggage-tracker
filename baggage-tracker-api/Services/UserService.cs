using BaggageTrackerApi.Entities;
using BaggageTrackerApi.Entities.DTOs;
using Microsoft.EntityFrameworkCore;

namespace BaggageTrackerApi.Services;

public class UserService(BaggageTrackerDbContext baggageTrackerDbContext)
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
    
    public List<UserDto> GetUsers()
    {
        var users = baggageTrackerDbContext.Users
            .Include(u => u.ActiveFlight)
            .Include(u => u.Baggages)
            .Select(ConvertToDto)
            .ToList();

        return users;
    }
    
    public UserDto? GetUserById(long userId)
    {
        var user = baggageTrackerDbContext.Users
            .Include(u => u.ActiveFlight)
            .Include(u => u.Baggages)
            .FirstOrDefault(u => u.Id == userId);

        if (user == null)
        {
            return null;
        }

        var dto = ConvertToDto(user);
        return dto;
    }
    
    public UserDto? CheckUserCredentials(string username, string hashedPassword)
    {
        var user = baggageTrackerDbContext.Users
            .SingleOrDefault(u => u.Name == username 
                                  && u.Password == hashedPassword);

        return user == null ? null :  ConvertToDto(user);
    }
}