using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using BaggageTrackerApi.Enums;

namespace BaggageTrackerApi.Entities.DTOs;

public class UserDto
{
    public required long Id { get; init; }

    [StringLength(50)]
    public required string Username { get; init; }

    [StringLength(150)]
    public required string FullName { get; init; }

    
    public required UserRole Role { get; init; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Flight? ActiveFlight { get; init; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ICollection<Baggage>? Baggages { get; init; }
}