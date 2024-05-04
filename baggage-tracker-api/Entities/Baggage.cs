using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using BaggageTrackerApi.Enums;

namespace BaggageTrackerApi.Entities;

[Table("bt_baggages")]
public class Baggage(long id, string tagNumber, long userId, BaggageStatus baggageStatus)
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; init; } = id;

    [StringLength(15)]
    public string TagNumber { get; init; } = tagNumber;

    [JsonIgnore]
    public long UserId { get; init; } = userId;
    
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public BaggageStatus BaggageStatus { get; init; } = baggageStatus;
    
    [ForeignKey("UserId")]
    [JsonIgnore]
    public virtual User User { get; init; }
}