using BaggageTrackerApi.Extensions;
using BaggageTrackerApi.Models.QrCode;

namespace BaggageTrackerApi.Services;

public class QrCodeDataProcessor
{
    public static readonly char UbcSeparator = '$';
    private static readonly byte UbcFragmentQuantity = 3;
    
    public UniqueBaggageCode ParseQrCodeData(string qrCodeData)
    {
        var fragments = qrCodeData.Split(UbcSeparator);

        if (fragments.Length != UbcFragmentQuantity)
        {
            throw new Exception(
                $"Faulty UBC code: Expected {UbcFragmentQuantity} fragments but found {fragments.Length}.");
        }

        return new UniqueBaggageCode(
            flightNumber: fragments[0],
            username: fragments[1],
            baggageId: fragments[2].ParseAsGuid());
    }
}