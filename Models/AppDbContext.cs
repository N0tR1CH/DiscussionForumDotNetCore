using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DiscussionForum.Models;

public class AppDbContext : IdentityDbContext<ApiUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Category entity
        modelBuilder.Entity<Category>().HasIndex(c => c.Name).IsUnique();
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Forum> Forums { get; set; }
}
