using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BaggageTrackerApi.Entities;

[Table("bt_flights")]
public sealed class Flight(string flightNumber, long userId)
{
    public Flight(long id, string flightNumber, long userId) : this(flightNumber, userId)
    {
        Id = id;
        FlightNumber = flightNumber;
        UserId = userId;
    }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; init; }

    [StringLength(15)]
    public string FlightNumber { get; init; } = flightNumber;

    [JsonIgnore]
    public long UserId { get; init; } = userId;

    [ForeignKey("UserId")]
    [JsonIgnore]
    public User User { get; init; } = null!;
}