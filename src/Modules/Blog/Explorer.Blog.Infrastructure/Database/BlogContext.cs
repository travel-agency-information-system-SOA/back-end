using Explorer.Blog.Core.Domain;
using Explorer.Blog.Core.Domain;
using Explorer.BuildingBlocks.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Blog.Infrastructure.Database;

public class BlogContext : DbContext
{
    public DbSet<BlogPost> BlogPosts { get; set; }
    public BlogContext(DbContextOptions<BlogContext> options) : base(options) {}


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("blog");

        // Configure JSONB for value objects
        modelBuilder.Entity<BlogPost>()
            .Property(b => b.Comments)  // Replace with the actual property for your first value object
            .HasColumnType("jsonb");

        modelBuilder.Entity<BlogPost>()
            .Property(b => b.Ratings)  // Replace with the actual property for your first value object
            .HasColumnType("jsonb");

    }
}