using BaggageTrackerApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace BaggageTrackerApi.Services;

public class MockDataService(BaggageTrackerDbContext baggageTrackerDbContext)
{
    private readonly BaggageTrackerDbContext _baggageTrackerDbContext = 
        baggageTrackerDbContext ?? throw new ArgumentNullException(nameof(baggageTrackerDbContext));

    public int PersistUserMockData()
    {
        var userExist = _baggageTrackerDbContext.Users.Any();

        if (userExist)
        {
            return -1;
        }
        
        _baggageTrackerDbContext.Users.AddRange(GetUserMockData());
        _baggageTrackerDbContext.SaveChanges();

        return 0;
    }
    
    public int PersistFlightMockData()
    {
        var flightExist = _baggageTrackerDbContext.Flights.Any();

        if (flightExist)
        {
            return -1;
        }
        
        _baggageTrackerDbContext.Flights.AddRange(GetFlightMockData());
        _baggageTrackerDbContext.SaveChanges();

        return 0;
    }

    public int PersistBaggageMockData()
    {
        var baggageExist = _baggageTrackerDbContext.Baggages.Any();

        if (baggageExist)
        {
            return -1;
        }
        
        _baggageTrackerDbContext.Baggages.AddRange(GetBaggageMockData());
        _baggageTrackerDbContext.SaveChanges();

        return 0;
    }

    public void DeleteUserMockData()
    {
        _baggageTrackerDbContext.Users.ExecuteDelete();
    }
    
    public void DeleteFlightMockData()
    {
        _baggageTrackerDbContext.Flights.ExecuteDelete();
    }
    
    public void DeleteBaggageMockData()
    {
        _baggageTrackerDbContext.Baggages.ExecuteDelete();
    }
    
    private static IEnumerable<User> GetUserMockData() =>
        new List<User>
        {
            new("Avery", "Thompson"),
            new("Sebastian", "Morales"),
            new("Olivia", "Martinez"),
        };
    
    private static IEnumerable<Flight> GetFlightMockData() =>
        new List<Flight>
        {
            new("TK5094", 1),
            new("TK5094", 2),
            new("TK2745", 3),
        };
    
    private static IEnumerable<Baggage> GetBaggageMockData() =>
        new List<Baggage>
        {
            new("T436712", 1),
            new("T377053", 1),
            new("T205967", 1),
            new("T519736", 2),
            new("T724821", 3),
            new("T541263", 3),
        };
}