using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BaggageTrackerApi.Entities;

[Table("bt_flights")]
public class Flight(long id, string flightNumber, long userId)
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; init; } = id;

    [StringLength(15)]
    public string FlightNumber { get; init; } = flightNumber;

    [JsonIgnore]
    public long UserId { get; init; } = userId;

    [ForeignKey("UserId")]
    [JsonIgnore]
    public virtual User User { get; init; }
}