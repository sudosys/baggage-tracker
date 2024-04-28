namespace BaggageTrackerApi.Services
{
    public class QrCodeGeneratorService
    {
        public static void GenerateQrCode(string flightNumber)
        {
            /*
             * 1. Fetch data by filtering FlightNumber
             * 2. Combining PassengerHash + FlightNumber + TagNumber, generate QR code
             * 3. Save the QR code to PROJECT_ROOT/qr_output
             */
        }
    }
}
