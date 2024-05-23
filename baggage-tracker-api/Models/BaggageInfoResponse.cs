using BaggageTrackerApi.Entities.DTOs;

namespace BaggageTrackerApi.Models;

public class BaggageInfoResponse(string flightNumber, List<BaggageDto> baggages)
{
    public string FlightNumber { get; set; } = flightNumber;

    public List<BaggageDto> Baggages { get; set; } = baggages;
}