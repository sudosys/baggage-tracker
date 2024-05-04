using BaggageTrackerApi.Entities;
using BaggageTrackerApi.Enums;
using Microsoft.EntityFrameworkCore;

namespace BaggageTrackerApi.Services;

public class UserService(BaggageTrackerDbContext baggageTrackerDbContext)
{
    public List<User> GetUsers(bool passengersOnly)
    {
        var users = baggageTrackerDbContext.Users
            .Include(u => u.ActiveFlight)
            .Include(u => u.Baggages)
            .Where(u => !passengersOnly || u.Role == UserRole.Passenger)
            .ToList();

        return users;
    }
    
    public User? GetUserById(long userId)
    {
        var user = baggageTrackerDbContext.Users
            .Include(u => u.ActiveFlight)
            .Include(u => u.Baggages)
            .FirstOrDefault(u => u.Id == userId);

        if (user == null)
        {
            return null;
        }

        return user;
    }
    
    public User? CheckUserCredentials(string username, string hashedPassword)
    {
        var user = baggageTrackerDbContext.Users
            .Include(u => u.ActiveFlight)
            .Include(u => u.Baggages)
            .SingleOrDefault(u => u.Username == username 
                                  && u.Password == hashedPassword);

        return user;
    }
}