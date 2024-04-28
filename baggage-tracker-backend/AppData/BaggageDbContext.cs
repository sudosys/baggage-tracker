using Microsoft.EntityFrameworkCore;

namespace BaggageTrackerApi.AppData
{
    public class BaggageDbContext : DbContext
    {
        public DbSet<BaggageTracker> BaggageTracker { get; set; }
    }
}
