using System.Reflection;

namespace BaggageTrackerApi.Services;

public class HelpService
{
    private const string HelpTextFileName = "HelpText.txt";

    public string? GetHelpText()
    {
        var assembly = Assembly.GetExecutingAssembly();

        var path = assembly.GetManifestResourceNames().SingleOrDefault(r => r.EndsWith(HelpTextFileName)) ?? string.Empty;
        var stream = assembly.GetManifestResourceStream(path);

        if (stream == null)
        {
            return null;
        } 

        var reader = new StreamReader(stream);

        return reader.ReadToEnd();
    }
}