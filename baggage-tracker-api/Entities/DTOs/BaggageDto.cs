using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using BaggageTrackerApi.Enums;

namespace BaggageTrackerApi.Entities.DTOs;

public class BaggageDto
{
    [MaxLength(150)]
    public string BaggageName { get; init; } = null!;

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public BaggageStatus BaggageStatus { get; init; }
}