using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaggageTrackerApi.Entities;

[Table("bt_flights")]
public class Flight
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [StringLength(15)]
    public string FlightNumber { get; set; }
    
    public long UserId { get; set; }
    
    [ForeignKey("UserId")]
    public virtual User User { get; set; }
}