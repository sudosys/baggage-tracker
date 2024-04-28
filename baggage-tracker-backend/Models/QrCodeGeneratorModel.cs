namespace BaggageTrackerApi.Models
{
    public class QrCodeGeneratorModel
    {
        public string PassengerHash { get; set; }

        public int FlightNumber { get; set; }

        public int TagNumber { get; set; }
    }
}
