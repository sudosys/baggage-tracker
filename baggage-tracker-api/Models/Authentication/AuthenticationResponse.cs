using System.Text.Json.Serialization;
using BaggageTrackerApi.Entities.DTOs;
using BaggageTrackerApi.Enums;

namespace BaggageTrackerApi.Models.Authentication;

public class AuthenticationResponse(AuthenticationStatus status, UserSlimDto? user, string? token)
{
    
    public AuthenticationStatus Status { get; init; } = status;
    
    public UserSlimDto? User { get; init; } = user;

    public string? Token { get; init; } = token;
}

