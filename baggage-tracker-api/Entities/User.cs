using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BaggageTrackerApi.Enums;

namespace BaggageTrackerApi.Entities;

[Table("bt_users")]
public class User(UserRole role, string username, string name, string surname)
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; init; }
    
    [StringLength(50)]
    public string Username { get; init; } = username;
    
    [StringLength(75)]
    public string Name { get; init; } = name;

    [StringLength(75)]
    public string Surname { get; init; } = surname;
    
    [MaxLength(20)]
    [MinLength(8)]
    public string Password { get; init; }

    public UserRole Role { get; init; }

    public virtual Flight ActiveFlight { get; set; }
    
    public virtual ICollection<Baggage> Baggages { get; set; }
}