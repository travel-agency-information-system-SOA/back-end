using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.TourExecutions;
using Explorer.Tours.Core.Domain.Tours;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using Explorer.Tours.Core.Domain.Problems;
using Explorer.Tours.Core.Domain.TourBundle;
using Explorer.Tours.Core.Domain.Competitions;

namespace Explorer.Tours.Infrastructure.Database;

public class ToursContext : DbContext
{
    public DbSet<Equipment> Equipment { get; set; }


    public DbSet<TouristEquipment> TouristEquipment { get; set; }


    public DbSet<Problem> Problems { get; set; }

    public DbSet<ProblemMessage> ProblemMessages { get; set; }


    public DbSet<TourReview> TourReviews { get; set; }


    public DbSet<GuideReview> GuideReviews { get; set; }
    public DbSet<Preferences> Preferences { get; set; }

    public DbSet<TourObject> TourObject { get; set; }

    public DbSet<ObjInTour> ObjInTours { get; set; }
    public DbSet<TourPoint> TourPoint { get; set; }
    public DbSet<PublicTourPoint> PublicTourPoint { get; set; }

    public DbSet<TourEquipment> TourEquipments { get; set; }

    public DbSet<Tour> Tours { get; set; }

    public DbSet<TourPointExecution> TourPointExecutions { get; set; }
    public DbSet<TourExecution> TourExecutions { get; set; }
    public DbSet<TourExecutionPosition> TourExecutionPositions { get; set; }

    public DbSet<TourBundle> TourBundles { get; set; }

	public DbSet<Competition> Competitions { get; set; }

	public DbSet<CompetitionApply> CompetitionApplies { get; set; }

	public ToursContext(DbContextOptions<ToursContext> options) : base(options) {}

   

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("tours");


        modelBuilder.Entity<Tour>().Property(item => item.TourCharacteristics).HasColumnType("jsonb");



        modelBuilder.Entity<TourExecution>()
            .HasOne(te => te.Position)
            .WithOne(p => p.Execution)
            .HasForeignKey<TourExecutionPosition>(p => p.TourExecutionId)
            .IsRequired();

        modelBuilder.Entity<TourExecution>()
        .HasMany(te => te.TourPoints)
        .WithOne(tep => tep.ТоurExecution)
        .HasForeignKey(tep => tep.TourExecutionId);

    }

   

}