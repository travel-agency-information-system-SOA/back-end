using Explorer.Blog.Core.Domain;
using Explorer.Blog.Core.Domain;
using Explorer.BuildingBlocks.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Blog.Infrastructure.Database;

public class BlogContext : DbContext
{
    public DbSet<BlogPost> BlogPosts { get; set; }
    public BlogContext(DbContextOptions<BlogContext> options) : base(options) {}

    public DbSet<BlogPostComment> BlogPostComments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("blog");
    }
}