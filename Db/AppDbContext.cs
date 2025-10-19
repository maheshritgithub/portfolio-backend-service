using Microsoft.EntityFrameworkCore;
using Portfolio.Service.Db.Models;
using System.Reflection.Emit;

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
    /// UserDetail Info for a user
    /// </summary>
    public DbSet<UserDetails> UserDetail { get; set; }

    /// <summary>
    /// User Experience details
    /// </summary>
    public DbSet<Experience> Experience { get; set; }


    /// <summary>
    /// Invoked during model creation.
    /// </summary>
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Username duplicate handling
        DefaultUserDbBindings(builder);

        DefaultUserDetailsDbBindings(builder);

        DefaulExperienceDbBindings(builder);

    }

    private static void DefaultUserDbBindings(ModelBuilder builder)
    {
        builder.Entity<User>()
            .HasIndex(u => u.Username)
            .IsUnique();
    }

    private static void DefaultUserDetailsDbBindings(ModelBuilder builder)
    {
        builder.Entity<UserDetails>()
           .HasOne<User>()
           .WithOne()
           .HasForeignKey<UserDetails>(a => a.UserId)
           .OnDelete(DeleteBehavior.Cascade);
    }
    private static void DefaulExperienceDbBindings(ModelBuilder builder)
    {
        builder.Entity<Experience>()
           .HasOne<User>()
           .WithOne()
           .HasForeignKey<Experience>(a => a.UserId)
           .OnDelete(DeleteBehavior.Cascade);
    }
}
