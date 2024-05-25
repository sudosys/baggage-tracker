namespace BaggageTrackerApi.Models.Registration;

public class PassengerCredential(string username, string password)
{
    public string Username { get; } = username;
    
    public string Password { get; } = password;
}