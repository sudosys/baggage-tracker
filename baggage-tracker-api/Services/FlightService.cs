using System.IO.Compression;
using System.Text;
using BaggageTrackerApi.Entities;
using BaggageTrackerApi.Enums;
using BaggageTrackerApi.Extensions;
using BaggageTrackerApi.Models;
using BaggageTrackerApi.Models.Registration;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace BaggageTrackerApi.Services;

public class FlightService(BaggageTrackerDbContext baggageTrackerDbContext, PasswordGenerator passwordGenerator)
{
    public async Task<MemoryStream> RegisterFlightManifests(List<FlightManifest> manifests, CancellationToken cancellationToken)
    {
        var credentialEntries = new List<(string fileName, List<PassengerCredential>)>();
        
        await baggageTrackerDbContext.Database.BeginTransactionAsync(cancellationToken);
        
        foreach (var manifest in manifests)
        {
            var passengerCredentials = new List<PassengerCredential>();
            
            foreach (var passengerInfo in manifest.Passengers)
            {
                var credential = CreateCredentialForPassenger(passengerInfo);
                passengerCredentials.Add(credential);
                RegisterPassenger(passengerInfo, credential, manifest.FlightNumber);
            }
            
            credentialEntries.Add(($"{manifest.FlightNumber}_passenger_credentials", passengerCredentials));
        }

        await baggageTrackerDbContext.SaveChangesAsync(cancellationToken);

        var compressed = CompressFlightManifests(credentialEntries);
        compressed.Seek(0, SeekOrigin.Begin);
        
        await baggageTrackerDbContext.Database.CommitTransactionAsync(cancellationToken);

        return compressed;
    }

    private static MemoryStream CompressFlightManifests(List<(string fileName, List<PassengerCredential>)> entries)
    {
        var archiveStream = new MemoryStream();

        using var archive = new ZipArchive(archiveStream, ZipArchiveMode.Create, true);
        foreach (var entry in entries)
        {
            var serialized = JsonConvert.SerializeObject(entry.Item2, Formatting.Indented);
            var serializedBytes = Encoding.UTF8.GetBytes(serialized);

            var archiveEntry = archive.CreateEntry($"{entry.fileName}.json");
            
            using var entryStream = archiveEntry.Open();
            entryStream.Write(serializedBytes, 0, serializedBytes.Length);
        }

        return archiveStream;
    }

    private PassengerCredential CreateCredentialForPassenger(PassengerRegistration passengerInfo) => 
        new(GenerateUsername(passengerInfo.FullName), passwordGenerator.GeneratePassword());

    private void RegisterPassenger(
        PassengerRegistration registrationInfo,
        PassengerCredential credential,
        string flightNumber)
    {
        var user = new User(
            UserRole.Passenger,
            credential.Username,
            registrationInfo.FullName,
            credential.Password.Sha256Hash());

        baggageTrackerDbContext.Users.Add(user);
        baggageTrackerDbContext.SaveChanges(); // to get the id early

        var activeFlight = new Flight(flightNumber, user.Id);

        var baggages = registrationInfo.Baggages
            .Select(baggage => 
                new Baggage(
                    Guid.NewGuid(),
                    baggage,
                    user.Id,
                    BaggageStatus.Undefined))
            .ToList();

        baggageTrackerDbContext.Flights.Add(activeFlight);
        baggageTrackerDbContext.Baggages.AddRange(baggages);
    }

    private static string GenerateUsername(string fullName)
    {
        var fragments = fullName
            .Split(' ')
            .Select(f => f.ToLower())
            .ToList();

        fragments.Add(StringExtensions.GetRandomNumberString());

        return string.Join('.', fragments);
    }

    public async Task<List<ActiveFlight>> GetActiveFlights(CancellationToken cancellationToken) =>
        await baggageTrackerDbContext.Flights
            .GroupBy(f => f.FlightNumber)
            .Select(f => new ActiveFlight(f.Key, f.Count()))
            .ToListAsync(cancellationToken);
}