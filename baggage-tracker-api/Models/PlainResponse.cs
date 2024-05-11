namespace BaggageTrackerApi.Models;

public struct PlainResponse(string message)
{
    public string Message { get; set; } = message;
}