namespace BaggageTrackerApi.Models;

public struct PlainResponse(object response)
{
    public object Response { get; set; } = response;
}