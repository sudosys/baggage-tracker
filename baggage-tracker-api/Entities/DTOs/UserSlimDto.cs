using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using BaggageTrackerApi.Enums;

namespace BaggageTrackerApi.Entities.DTOs;

public class UserSlimDto
{
    public required long Id { get; init; }

    [StringLength(50)]
    public required string Username { get; init; }

    [StringLength(150)]
    public required string FullName { get; init; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public required UserRole Role { get; init; }
}