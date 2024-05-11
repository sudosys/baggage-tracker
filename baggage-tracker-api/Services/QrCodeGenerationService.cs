using System.IO.Compression;
using BaggageTrackerApi.Entities;
using BaggageTrackerApi.Entities.DTOs;
using BaggageTrackerApi.Extensions;
using BaggageTrackerApi.Models.QrCode;
using QRCoder;

namespace BaggageTrackerApi.Services;

public class QrCodeGenerationService(QRCodeGenerator qrCodeGenerator)
{
    public Dictionary<string, List<QrCodeFile>> CreateQrCodes(IEnumerable<UserDto> passengersOfFlight)
    {
        var qrCodesGroupedByPassenger = new Dictionary<string, List<QrCodeFile>>();

        foreach (var passenger in passengersOfFlight)
        {
            foreach (var baggage in passenger.Baggages.EmptyIfNull())
            {
                var uniqueBaggageCode = GenerateUniqueBaggageCode(
                    passenger.ActiveFlight!.FlightNumber,
                    passenger.Id,
                    baggage.BaggageId.ToString());

                var qrCode = GenerateQrCode(uniqueBaggageCode);

                AddQrCodeToFolder(qrCodesGroupedByPassenger, passenger, baggage, qrCode);
            }
        }

        return qrCodesGroupedByPassenger;
    }

    private static void AddQrCodeToFolder(
        IDictionary<string, List<QrCodeFile>> qrCodes,
        UserDto passenger, 
        Baggage baggage,
        byte[] qrCode)
    {
        var qrCodeFileName = GetQrCodeFileName(baggage);
        if (qrCodes.TryGetValue(passenger.Username, out var qrCodesOfPassenger))
        {
            qrCodesOfPassenger.Add(new QrCodeFile(qrCodeFileName, qrCode));
        }
        else
        {
            qrCodes.Add(passenger.Username, [new QrCodeFile(qrCodeFileName, qrCode)]);
        }
    }

    private static string GetQrCodeFileName(Baggage baggage) => $"{baggage.BaggageName}_{baggage.BaggageId}.png"; 
    
    public static MemoryStream CompressQrCodes(Dictionary<string, List<QrCodeFile>> qrCodes)
    {
        var archiveStream = new MemoryStream();

        using var archive = new ZipArchive(archiveStream, ZipArchiveMode.Create, leaveOpen: true);
        
        foreach (var folder in qrCodes)
        {
            foreach (var qrCode in folder.Value)
            {
                var fileName = Path.Combine(folder.Key, qrCode.Name);
                var zipEntry = archive.CreateEntry(fileName);

                using var entryStream = zipEntry.Open();
                using var qrCodeStream = new MemoryStream(qrCode.Content);
                qrCodeStream.CopyTo(entryStream);
            }
        }
        
        return archiveStream;
    }

    private static string GenerateUniqueBaggageCode(string flightNumber, long userId, string baggageId) 
        => string.Join(UbcProcessor.UbcSeparator, flightNumber, userId, baggageId);

    private byte[] GenerateQrCode(string qrContent)
    {
        var data = qrCodeGenerator.CreateQrCode(qrContent, QRCodeGenerator.ECCLevel.Q);
        var qrCode = new PngByteQRCode(data);
        return qrCode.GetGraphic(20);
    }
}