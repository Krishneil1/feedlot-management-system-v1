// -------------------------------------------------------------------------------------------------
// FeedlotDbContext.cs -- The FeedlotDbContext.cs class.
// -------------------------------------------------------------------------------------------------

namespace FeedlotApi.Data;

using FeedlotApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

public class FeedlotDbContext : DbContext
{
    public FeedlotDbContext(DbContextOptions<FeedlotDbContext> options)
        : base(options)
    {

        if (Database.IsRelational() && Database.GetPendingMigrations().Any())
        {
            // Apply any pending migrations automatically on startup (use with caution in production)
            Database.Migrate();
        }
    }

    public DbSet<Animal> Animals => Set<Animal>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure the Animal table
        modelBuilder.Entity<Animal>(entity =>
        {
            entity.ToTable("Animals");

            entity.HasKey(a => a.Id);

            entity.Property(a => a.TagId)
                  .IsRequired()
                  .HasMaxLength(100);

            entity.Property(a => a.Breed)
                  .IsRequired()
                  .HasMaxLength(100);

            entity.Property(a => a.DateOfBirth)
                  .IsRequired();

            entity.Property(a => a.Synced)
                  .HasDefaultValue(false);
        });

        // You can configure more entities here in the future
    }
}
