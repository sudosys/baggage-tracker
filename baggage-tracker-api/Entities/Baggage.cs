using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaggageTrackerApi.Entities;

[Table("bt_baggages")]
public class Baggage
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [StringLength(15)]
    public string TagNumber { get; set; }
    
    public long UserId { get; set; }
    
    [ForeignKey("UserId")]
    public virtual User User { get; set; }
}