using Microsoft.EntityFrameworkCore;
using SyskeySoftlabs.Scribbler.Service.Db;

namespace Portfolio.Service.Db;

/// <summary>
/// Application database Context.
/// </summary>
public partial class AppDbContext : DbContext
{
    /// <summary>
    /// Application configuration.
    /// </summary>
    public DbSet<AppConfig> AppConfig { get; set; }

    /// <summary>
    /// Constructor for AppDbContext.
    /// </summary>
    /// <param name="options">DbContextOptions object.</param>
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    /// <summary>
    /// Invoked during the model creation.
    /// </summary>
    /// <param name="builder">The model builder object.</param>
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<AppConfig>();
        //Default data seeding 
    }
}
