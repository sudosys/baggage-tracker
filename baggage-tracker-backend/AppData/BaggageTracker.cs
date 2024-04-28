using System.ComponentModel.DataAnnotations;

namespace BaggageTrackerApi.AppData
{
    public class BaggageTracker
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string Surname { get; set; }

        [MaxLength(50)]
        public string FlightNumber { get; set; }

        [MaxLength(50)]
        public string TagNumber { get; set; }

        [MaxLength(1000)]
        public string InfoBaggage { get; set; }

        public bool Status { get; set; }

    }
}
