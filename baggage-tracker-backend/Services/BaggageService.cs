using System;
using System.Collections;

namespace BaggageTrackerApi.Services
{
    public class BaggageService
    {
        public static bool CheckBaggagePossession(string ubc, string passengerHash)
        {
            throw new NotImplementedException();
        }

        private static string GetBaggageStatus(int statusNumber)
        {
            return statusNumber switch
            {
                0 => "In the plane",
                1 => "Received by the passenger",
                2 => "In the lost office",
                3 => "Waiting for transfer to the plane",
                4 => "Unloaded from the plane",
                _ => ""
            };
        }

        public static ArrayList CheckBaggageStatus(string passengerHash)
        {
            throw new NotImplementedException();
        }

        public static void SetBaggageStatus(string ubc, string status)
        {
            throw new NotImplementedException();
        }
    }
}
