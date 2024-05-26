using BaggageTrackerApi.Exceptions;
using BaggageTrackerApi.Extensions;
using BaggageTrackerApi.Models.QrCode;

namespace BaggageTrackerApi.Services;

public class UbcProcessor(BaggageTrackerDbContext baggageTrackerDbContext)
{
    public static readonly char UbcSeparator = '$';
    public static readonly byte UbcFragmentQuantity = 3;
    
    public UniqueBaggageCode ParseUbc(string qrCodeData)
    {
        var fragments = qrCodeData.Split(UbcSeparator);

        if (fragments.Length != UbcFragmentQuantity)
        {
            throw new FaultyUbcCodeException(fragments.Length);
        }

        return new UniqueBaggageCode(
            flightNumber: fragments[0],
            userId: long.Parse(fragments[1]),
            baggageId: fragments[2].ParseAsGuid());
    }

    public bool ValidateUbc(UniqueBaggageCode ubc) =>
        DoesUserExist(ubc.UserId) &&
        IsUserInFlight(ubc.UserId, ubc.FlightNumber) &&
        DoesBaggageExist(ubc.BaggageId);

    private bool DoesUserExist(long userId) => 
        baggageTrackerDbContext.Users.Any(u => u.Id == userId);

    private bool IsUserInFlight(long userId, string flightNumber) =>
        baggageTrackerDbContext.Flights.Any(f => f.FlightNumber == flightNumber && f.UserId == userId);
 
    private bool DoesBaggageExist(Guid baggageId) => 
        baggageTrackerDbContext.Baggages.Any(b => b.BaggageId == baggageId);
}