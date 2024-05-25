namespace BaggageTrackerApi.Models.PasswordGeneration;

public struct PasswordGenerationSettings(
    bool includeSymbols,
    bool includeNumbers,
    bool includeUpperCase,
    bool includeLowerCase,
    int length)
{
    public PasswordGenerationSettings() : 
        this(
            includeSymbols: true,
            includeNumbers: true,
            includeUpperCase: true,
            includeLowerCase: true,
            length: DefaultLength)
    {
    }
    
    public int Length { get; set; } = length;

    private const int DefaultLength = 8;
    public bool IncludeSymbols { get; set; } = includeSymbols;
    public bool IncludeNumbers { get; set; } = includeNumbers;
    public bool IncludeUpperCase { get; set; } = includeUpperCase;
    public bool IncludeLowerCase { get; set; } = includeLowerCase;
}