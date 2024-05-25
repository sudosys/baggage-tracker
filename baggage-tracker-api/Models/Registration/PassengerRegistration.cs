using System.ComponentModel.DataAnnotations;

namespace BaggageTrackerApi.Models.Registration;

public class PassengerRegistration(string fullName, string[] baggages)
{
    [StringLength(150)]
    public required string FullName { get; init; } = fullName;

    public string[] Baggages { get; init; } = baggages;
}