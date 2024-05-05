using System.Security.Cryptography;
using System.Text;

namespace BaggageTrackerApi.Extensions;

public static class StringExtensions
{
    public static string Sha256Hash(this string input)
    {
        var bytes = Encoding.UTF8.GetBytes(input);
        var hashed = SHA256.HashData(bytes);

        if (hashed == null)
        {
            throw new ArgumentNullException();
        }

        var hashString = new StringBuilder();

        foreach (var @byte in hashed)
        {
            hashString.Append(@byte.ToString("x2"));
        }
            
        return hashString.ToString();
    }

    public static Guid ParseAsGuid(this string input) => Guid.Parse(input);
}