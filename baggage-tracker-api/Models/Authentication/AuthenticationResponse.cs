using System.Text.Json.Serialization;
using BaggageTrackerApi.Entities;
using BaggageTrackerApi.Enums;

namespace BaggageTrackerApi.Models.Authentication;

public class AuthenticationResponse(AuthenticationStatus status, User? user, string? token)
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public AuthenticationStatus Status { get; init; } = status;
    
    public User? User { get; init; } = user;

    public string? Token { get; init; } = token;
}

