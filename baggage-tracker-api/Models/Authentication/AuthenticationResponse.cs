using BaggageTrackerApi.Entities.DTOs;
using BaggageTrackerApi.Enums;

namespace BaggageTrackerApi.Models.Authentication;

public class AuthenticationResponse(AuthenticationStatus status, UserDto? user, string? token)
{
    public AuthenticationStatus Status { get; set; }
    
    public UserDto? User { get; set; } = user;

    public string? Token { get; set; } = token;
}

