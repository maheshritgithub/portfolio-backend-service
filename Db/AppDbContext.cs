using System.Text.Json;
using Portfolio.Service.Db.Models;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Portfolio.Entities.RequestModel;

namespace Portfolio.Service.Db
{
    /// <summary>
    /// Application database context.
    /// </summary>
    public partial class AppDbContext : DbContext
    {
        private static readonly JsonSerializerOptions SerializerOptions;

        static AppDbContext()
        {
            SerializerOptions = new JsonSerializerOptions
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Application users.
        /// </summary>
        public DbSet<User> User { get; set; }

        /// <summary>
        /// User details info for a user.
        /// </summary>
        public DbSet<UserDetails> UserDetail { get; set; }

        /// <summary>
        /// User experience details.
        /// </summary>
        public DbSet<Experience> Experience { get; set; }

        /// <summary>
        /// Project details.
        /// </summary>
        public DbSet<Project> Project { get; set; }

        /// <summary>
        /// Invoked during model creation.
        /// </summary>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            DefaultUserDbBindings(builder);
            DefaultUserDetailsDbBindings(builder);
            DefaultExperienceDbBindings(builder);
            DefaultProjectDbBindings(builder);
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

        private static void DefaultExperienceDbBindings(ModelBuilder builder)
        {
            builder.Entity<Experience>()
                .HasOne<User>()
                .WithOne()
                .HasForeignKey<Experience>(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        private static void DefaultProjectDbBindings(ModelBuilder builder)
        {
            builder.Entity<Project>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Project>()
                .Property(d => d.Image)
                .HasConversion(
                serialize => JsonSerializer.Serialize(serialize, SerializerOptions),
                deserialize => JsonSerializer.Deserialize<ProjectImage>(deserialize, SerializerOptions)!);
        }
    }
}
