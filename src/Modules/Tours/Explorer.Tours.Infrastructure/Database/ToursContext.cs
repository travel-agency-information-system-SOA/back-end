using Explorer.Tours.Core.Domain;
using Microsoft.EntityFrameworkCore;
namespace Explorer.Tours.Infrastructure.Database;

public class ToursContext : DbContext
{
    public DbSet<Equipment> Equipment { get; set; }

    public DbSet<TourObject> TourObject { get; set; }

<<<<<<< HEAD
    public DbSet<ObjInTour> ObjInTours { get; set; }
=======
    public DbSet<TourPoint> TourPoint { get; set; }

    public DbSet<TourEquipment> TourEquipments { get; set; }
    public DbSet<TourKeyPoint> TourKeyPoints { get; set; }

    public DbSet<Tour> Tours { get; set; }
>>>>>>> e3b022f87b0a07bf6f699568991aac84175f429d

    public ToursContext(DbContextOptions<ToursContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("tours");
    }
}