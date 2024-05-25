namespace BaggageTrackerApi.Models.PasswordGeneration;

public static class PasswordGenerationPool
{
    public static string LowerCase => "abcdefghijklmnopqrstuvwxyz";
    public static string UpperCase => "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    public static string Numbers => "1234567890";
    public static string Symbols => "!@#$%^&*()";
}