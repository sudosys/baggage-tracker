using System.Security.Cryptography;
using System.Text;

namespace BaggageTrackerApi.Extensions;

public static class StringExtensions
{
    public static string Sha256Hash(this string input)
    {
        var bytes = Encoding.UTF8.GetBytes(input);
        var hashed = SHA256.HashData(bytes);

        var hashString = new StringBuilder();

        foreach (var @byte in hashed)
        {
            hashString.Append(@byte.ToString("x2"));
        }
            
        return hashString.ToString();
    }
    
    public static string GetRandomNumberString()
    {
        const int minValue = 100;

        var randomEngine = new Random();

        return randomEngine.Next(minValue, int.MaxValue).ToString();
    }
    
    public static Guid ParseAsGuid(this string input) => Guid.Parse(input);
}