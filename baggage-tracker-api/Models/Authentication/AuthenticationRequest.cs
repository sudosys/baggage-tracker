namespace BaggageTrackerApi.Models.Authentication;

public class AuthenticationRequest(string username, string password)
{
    public string Username { get; init; } = username;

    public string Password { get; init; } = password;
}