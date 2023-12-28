using Explorer.Stakeholders.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace Explorer.Stakeholders.Infrastructure.Database;

public class StakeholdersContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Person> People { get; set; }
    public DbSet<AppRating> AppRatings { get; set; }
    public DbSet<Club> Clubs { get; set; }

    public DbSet<TourPointRequest> TourPointRequests { get; set; }
    public DbSet<TourObjectRequest>TourObjectRequests { get; set; }
    public DbSet<RequestResponseNotification> RequestResponseNotifications { get; set; }

    public DbSet<UserPosition> UserPositions { get; set; }

    public DbSet<TouristXP> TouristXP { get; set; }
    public DbSet<UserMileage> UserMileages { get; set; }
    public DbSet<UserTourMileage> UserTourMileages { get; set; }

    public DbSet<PasswordReset> PasswordResets { get; set; }
    public DbSet<Follower> Followers { get; set; }
    public DbSet<FollowerMessage> FollowerMessages { get; set; }

    public StakeholdersContext(DbContextOptions<StakeholdersContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("stakeholders");

        modelBuilder.Entity<User>().HasIndex(u => u.Username).IsUnique();

        ConfigureStakeholder(modelBuilder);
    }

    private static void ConfigureStakeholder(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>()
            .HasOne<User>()
            .WithOne()
            .HasForeignKey<Person>(s => s.UserId);

        modelBuilder.Entity<PasswordReset>()
            .HasOne<User>()
            .WithOne()
            .HasForeignKey<PasswordReset>(s => s.UserId);
    }
}