using BaggageTrackerApi.Entities;
using BaggageTrackerApi.Enums;
using Microsoft.EntityFrameworkCore;

namespace BaggageTrackerApi;

public class BaggageTrackerDbContext(DbContextOptions<BaggageTrackerDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Flight> Flights { get; set; }
    public DbSet<Baggage> Baggages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ConstructRelations(modelBuilder);

        AddSeedData(modelBuilder);

        base.OnModelCreating(modelBuilder);
    }

    private static void ConstructRelations(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasOne(u => u.ActiveFlight)
            .WithOne(f => f.User)
            .HasForeignKey<Flight>(f => f.UserId);

        modelBuilder.Entity<User>()
            .HasMany(u => u.Baggages)
            .WithOne(b => b.User)
            .HasForeignKey(b => b.UserId);
    }

    private static void AddSeedData(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasData(new List<User>
        {
            new(UserRole.Passenger, "avery.thompson", "Avery", "Thompson"),
            new(UserRole.Passenger, "sebastian.morales", "Sebastian", "Morales"),
            new(UserRole.Passenger, "olivia.martinez", "Olivia", "Martinez"),
        });
        
        modelBuilder.Entity<Flight>().HasData(new List<Flight>
        {            
            new("TK5094", 1),
            new("TK5094", 2),
            new("TK2745", 3),
        });
        
        modelBuilder.Entity<Baggage>().HasData(new List<Baggage>
        {
            new("T436712", 1, BaggageStatus.Undefined),
            new("T377053", 1, BaggageStatus.Undefined),
            new("T205967", 1, BaggageStatus.Undefined),
            new("T519736", 2, BaggageStatus.Undefined),
            new("T724821", 3, BaggageStatus.Undefined),
            new("T541263", 3, BaggageStatus.Undefined),
        });
    }
}