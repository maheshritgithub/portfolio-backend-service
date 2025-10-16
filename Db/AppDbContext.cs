using Microsoft.EntityFrameworkCore;

namespace Portfolio.Service.Db;

/// <summary>
/// Application database context.
/// </summary>
public partial class AppDbContext(DbContextOptions<AppDbContext> options)
    : DbContext(options)
{
    /// <summary>
    /// Application users.
    /// </summary>
    public DbSet<User> User { get; set; }

    /// <summary>
    /// Invoked during model creation.
    /// </summary>
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Username duplicate handling
        builder.Entity<User>()
            .HasIndex(u => u.Username)
            .IsUnique();
    }
}
