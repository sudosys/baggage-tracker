using AutoMapper;
using BaggageTrackerApi.Entities;
using BaggageTrackerApi.Entities.DTOs;
using BaggageTrackerApi.Enums;
using BaggageTrackerApi.Extensions;
using BaggageTrackerApi.Models.Registration;

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

    public List<PassengerCredential> RegisterFlightManifest(FlightManifest manifest)
    {
        var passengerCredentials = new List<PassengerCredential>();
        
        foreach (var registrationInfo in manifest.Passengers)
        {
            var credential = CreateCredentialForPassenger(registrationInfo);
            passengerCredentials.Add(credential);
            RegisterPassenger(registrationInfo, credential, manifest.FlightNumber);
        }

        baggageTrackerDbContext.SaveChanges();

        return passengerCredentials;
    }

    private PassengerCredential CreateCredentialForPassenger(PassengerRegistration passengerInfo) => 
        new(GenerateUsername(passengerInfo.FullName), passwordGenerator.GeneratePassword());

    private void RegisterPassenger(
        PassengerRegistration registrationInfo,
        PassengerCredential credential,
        string flightNumber)
    {
        var user = new User(
            UserRole.Passenger,
            credential.Username,
            registrationInfo.FullName,
            credential.Password.Sha256Hash());

        baggageTrackerDbContext.Users.Add(user);
        baggageTrackerDbContext.SaveChanges(); // to get the id early

        var activeFlight = new Flight(flightNumber, user.Id);

        var baggages = registrationInfo.Baggages
            .Select(baggage => 
                new Baggage(
                    Guid.NewGuid(),
                    baggage,
                    user.Id,
                    BaggageStatus.Undefined))
            .ToList();

        baggageTrackerDbContext.Flights.Add(activeFlight);
        baggageTrackerDbContext.Baggages.AddRange(baggages);
    }

    private static string GenerateUsername(string fullName)
    {
        var fragments = fullName
            .Split(' ')
            .Select(f => f.ToLower())
            .ToList();

        fragments.Add(StringExtensions.GetRandomNumberString());

        return string.Join('.', fragments);
    }

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