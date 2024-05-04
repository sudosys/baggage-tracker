using System.ComponentModel.DataAnnotations;

namespace BaggageTrackerApi.Models.Registration;

public class UserRegistration(string username, string fullName, string password, string flightNumber, string[] baggages)
{
    [StringLength(50)]
    public required string Username { get; init; } = username;

    [StringLength(150)]
    public required string FullName { get; init; } = fullName;

    [MaxLength(32)]
    [MinLength(8)]
    public required string Password { get; init; } = password;

    public string FlightNumber { get; init; } = flightNumber;

    public string[] Baggages { get; init; } = baggages;
}