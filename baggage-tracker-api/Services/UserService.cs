using BaggageTrackerApi.Entities;
using BaggageTrackerApi.Enums;
using BaggageTrackerApi.Extensions;
using BaggageTrackerApi.Models.Registration;
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

    public List<User> GetUsersByFlightNumber(string flightNumber)
    {
        if (!baggageTrackerDbContext.DoesFlightExist(flightNumber))
        {
            throw new Exception($"Flight {flightNumber} does not exist.");
        }
        
        var usersByFlightNumber = baggageTrackerDbContext.Users
            .Include(u => u.ActiveFlight)
            .Include(u => u.Baggages)
            .Where(u => u.ActiveFlight != null && 
                        u.ActiveFlight.FlightNumber == flightNumber &&
                        u.Role == UserRole.Passenger)
            .ToList();

        return usersByFlightNumber;
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

    public void RegisterUser(UserRegistration userReg)
    {
        var user = new User(
            UserRole.Passenger,
            userReg.Username,
            userReg.FullName,
            userReg.Password.Sha256Hash());

        baggageTrackerDbContext.Users.Add(user);
        baggageTrackerDbContext.SaveChanges();

        var activeFlight = new Flight(userReg.FlightNumber, user.Id);

        var baggages = userReg.Baggages
            .Select(baggage => 
                new Baggage(
                    Guid.NewGuid(),
                    baggage,
                    user.Id,
                    BaggageStatus.Undefined))
            .ToList();

        baggageTrackerDbContext.Flights.Add(activeFlight);
        baggageTrackerDbContext.Baggages.AddRange(baggages);

        baggageTrackerDbContext.SaveChanges();
    }

    public void DeleteUser(long userId)
    {
        var user = baggageTrackerDbContext.Users.Where(u => u.Id == userId)
            .Include(u => u.ActiveFlight)
            .Include(u => u.Baggages)
            .SingleOrDefault();

        if (user == null)
        {
            throw new NullReferenceException($"User with the id {userId} could not be found.");
        }

        if (user.Role == UserRole.Personnel)
        {
            throw new InvalidOperationException($"Users with the role of {nameof(UserRole.Personnel)}.");
        }

        baggageTrackerDbContext.Users.Remove(user);
        baggageTrackerDbContext.SaveChanges();
    }
}