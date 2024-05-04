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
            new(1, UserRole.Passenger, "avery.thompson", "Avery Thompson", "711f24d8676c4462bcb9b8d6ff12e524483afcff5ea9ba726fba772c296b214c"),
            new(2, UserRole.Passenger, "sebastian.morales", "Sebastian Morales", "46498d3d669434f320a45770a9b8ab8cbc16bd7dfeeb724c5503b2cb9d3d395e"),
            new(3, UserRole.Passenger, "olivia.martinez", "Olivia Martinez", "0cc4bbe5ac4df909798c2ccd0844f15a86a694457758e42d9ce52e7d39e9e256"),
        });
        
        modelBuilder.Entity<Flight>().HasData(new List<Flight>
        {            
            new(1, "TK5094", 1),
            new(2, "TK5094", 2),
            new(3, "TK2745", 3),
        });
        
        modelBuilder.Entity<Baggage>().HasData(new List<Baggage>
        {
            new(Guid.NewGuid(), "Blue Samsonite Case", 1, BaggageStatus.Undefined),
            new(Guid.NewGuid(), "Benetti Sports Bag", 1, BaggageStatus.Undefined),
            new(Guid.NewGuid(), "Lightweight PP Collection", 1, BaggageStatus.Undefined),
            new(Guid.NewGuid(), "Samsonite Popsoda", 2, BaggageStatus.Undefined),
            new(Guid.NewGuid(), "Fantana Matrix PP Hard Shell", 3, BaggageStatus.Undefined),
            new(Guid.NewGuid(), "Canvas Explorer Holdall", 3, BaggageStatus.Undefined),
        });
    }
}