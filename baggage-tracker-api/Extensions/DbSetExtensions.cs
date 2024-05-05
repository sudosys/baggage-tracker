using BaggageTrackerApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace BaggageTrackerApi.Extensions;

public static class DbSetExtensions
{
    public static IQueryable<User> QueryUserWithFlightData(this DbSet<User> userDbContext) =>
        userDbContext
            .Include(u => u.ActiveFlight)
            .Include(u => u.Baggages);
}