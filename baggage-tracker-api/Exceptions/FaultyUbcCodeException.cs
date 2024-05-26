using BaggageTrackerApi.Services;

namespace BaggageTrackerApi.Exceptions;

public class FaultyUbcCodeException(int fragmentCount) : 
    ApiDomainException($"Faulty UBC code: Expected {UbcProcessor.UbcFragmentQuantity} fragments but found {fragmentCount}.");