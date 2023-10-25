using Explorer.Tours.Core.Domain;
using Microsoft.EntityFrameworkCore;
namespace Explorer.Tours.Infrastructure.Database;

public class ToursContext : DbContext
{
    public DbSet<Equipment> Equipment { get; set; }

    public DbSet<TourObject> TourObject { get; set; }

    public DbSet<ObjInTour> ObjInTours { get; set; }
    public DbSet<TourPoint> TourPoint { get; set; }

    public DbSet<TourEquipment> TourEquipments { get; set; }

    public DbSet<Tour> Tours { get; set; }

    public ToursContext(DbContextOptions<ToursContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("tours");
    }
}