using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using BaggageTrackerApi.Enums;

namespace BaggageTrackerApi.Entities;

[Table("bt_baggages")]
public sealed class Baggage(Guid baggageId, string baggageName, long userId, BaggageStatus baggageStatus)
{
    public Guid BaggageId { get; init; } = baggageId;

    [MaxLength(150)]
    public string BaggageName { get; init; } = baggageName;

    [JsonIgnore]
    public long UserId { get; init; } = userId;

    
    public BaggageStatus BaggageStatus { get; set; } = baggageStatus;
    
    [ForeignKey("UserId")]
    [JsonIgnore]
    public User User { get; init; } = null!;
}