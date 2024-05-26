using AutoMapper;
using BaggageTrackerApi.Entities.DTOs;
using BaggageTrackerApi.Enums;
using BaggageTrackerApi.Extensions;

namespace BaggageTrackerApi.Services;

public class UserService(BaggageTrackerDbContext baggageTrackerDbContext, IMapper mapper, PasswordGenerator passwordGenerator)
{
    public IEnumerable<UserDto> GetUsers(bool passengersOnly) =>
        baggageTrackerDbContext.Users
            .QueryUserWithFlightData()
            .Where(u => !passengersOnly || u.Role == UserRole.Passenger)
            .AsEnumerable()
            .Select(mapper.Map<UserDto>)
            .ToList();

    public UserDto? GetUserById(long userId) =>
        baggageTrackerDbContext.Users
            .QueryUserWithFlightData()
            .AsEnumerable()
            .Select(mapper.Map<UserDto>)
            .FirstOrDefault(u => u.Id == userId);

    public UserSlimDto? CheckUserCredentials(string username, string hashedPassword) =>
        baggageTrackerDbContext.Users
            .Where(u => u.Username == username
                        && u.Password == hashedPassword)
            .AsEnumerable()
            .Select(mapper.Map<UserSlimDto>)
            .SingleOrDefault();

    public void DeleteUser(long userId)
    {
        var user = baggageTrackerDbContext.Users
            .QueryUserWithFlightData()
            .SingleOrDefault(u => u.Id == userId);

        if (user == null)
        {
            throw new NullReferenceException($"User with the id {userId} could not be found.");
        }

        if (user.Role == UserRole.Personnel)
        {
            throw new InvalidOperationException($"Users with the role of {nameof(UserRole.Personnel)} can't be deleted.");
        }

        baggageTrackerDbContext.Users.Remove(user);
        baggageTrackerDbContext.SaveChanges();
    }
}