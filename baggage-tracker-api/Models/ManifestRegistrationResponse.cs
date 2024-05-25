using BaggageTrackerApi.Enums;
using BaggageTrackerApi.Models.Registration;

namespace BaggageTrackerApi.Models;

public class ManifestRegistrationResponse(ManifestRegistrationStatus status, List<PassengerCredential> passengerCredentials)
{
    public ManifestRegistrationStatus status { get; set; } = status;
    
    public List<PassengerCredential> PassengerCredentials { get; set; } = passengerCredentials;
}