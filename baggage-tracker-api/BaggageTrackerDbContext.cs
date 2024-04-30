using BaggageTrackerApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace BaggageTrackerApi;

public class BaggageTrackerDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Flight> Flights { get; set; }
    public DbSet<Baggage> Baggages { get; set; }

    public BaggageTrackerDbContext(DbContextOptions<BaggageTrackerDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasOne(u => u.ActiveFlight)
            .WithOne(f => f.User)
            .HasForeignKey<Flight>(f => f.UserId);

        modelBuilder.Entity<User>()
            .HasMany(u => u.Baggages)
            .WithOne(b => b.User)
            .HasForeignKey(b => b.UserId);

        base.OnModelCreating(modelBuilder);
    }
}