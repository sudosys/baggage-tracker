using System.Collections.Generic;

namespace BaggageTrackerApi.Models
{
    public class User
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public string Surname { get; set; }
        
        public string FlightNumber { get; set; }
        
        public string TagNumber { get; set; }
        
        public string BaggageInfo { get; set; }
        
        public bool Status { get; set; } 
        
        public List<User> BaggageList { get; set; }
    }
}
