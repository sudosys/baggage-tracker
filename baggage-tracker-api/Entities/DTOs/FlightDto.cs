using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaggageTrackerApi.Entities.DTOs;

public class FlightDto
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; init; }

    [StringLength(15)]
    public string FlightNumber { get; init; } = null!;
}