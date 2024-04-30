using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaggageTrackerApi.Entities;

[Table("bt_flights")]
public class Flight(string flightNumber, long userId)
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [StringLength(15)]
    public string FlightNumber { get; set; } = flightNumber;

    public long UserId { get; set; } = userId;

    [ForeignKey("UserId")]
    public virtual User User { get; set; }
}