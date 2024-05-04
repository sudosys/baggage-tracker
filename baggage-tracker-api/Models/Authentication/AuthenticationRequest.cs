namespace BaggageTrackerApi.Models.Authentication;

public class AuthenticationRequest(string username, string password)
{
    public string Username { get; set; } = username;

    public string Password { get; set; } = password;
}