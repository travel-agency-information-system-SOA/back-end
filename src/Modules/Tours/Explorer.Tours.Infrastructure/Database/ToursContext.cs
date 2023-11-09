using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.Tours;
using Microsoft.EntityFrameworkCore;
namespace Explorer.Tours.Infrastructure.Database;

public class ToursContext : DbContext
{
    public DbSet<Equipment> Equipment { get; set; }


    public DbSet<TouristEquipment> TouristEquipment { get; set; }


    public DbSet<Problem> Problems { get; set; }



    public DbSet<TourReview> TourReviews { get; set; }


    public DbSet<GuideReview> GuideReviews { get; set; }
    public DbSet<Preferences> Preferences { get; set; }

    public DbSet<TourObject> TourObject { get; set; }

    public DbSet<ObjInTour> ObjInTours { get; set; }
    public DbSet<TourPoint> TourPoint { get; set; }

    public DbSet<TourEquipment> TourEquipments { get; set; }

    public DbSet<Tour> Tours { get; set; }



    public ToursContext(DbContextOptions<ToursContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("tours");
        modelBuilder.Entity<Tour>().Property(item => item.TourCharacteristics).HasColumnType("jsonb");
    }

   

}