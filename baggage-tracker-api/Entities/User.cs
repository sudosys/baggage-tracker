using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaggageTrackerApi.Entities;

[Table("bt_users")]
public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    
    [StringLength(75)]
    public string Name { get; set; }
    
    [StringLength(75)]
    public string Surname { get; set; }
    
    public virtual Flight ActiveFlight { get; set; }
    
    public virtual ICollection<Baggage> Baggages { get; set; }
}