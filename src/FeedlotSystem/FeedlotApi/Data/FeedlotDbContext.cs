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

    public DbSet<Booking> Bookings => Set<Booking>();

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
        modelBuilder.Entity<Booking>(entity =>
        {
            entity.ToTable("Bookings");

            entity.HasKey(b => b.Id);
            entity.Property(b => b.BookingNumber).IsRequired().HasMaxLength(100);
            entity.Property(b => b.VendorName).IsRequired().HasMaxLength(100);
            entity.Property(b => b.Property).HasMaxLength(100);
            entity.Property(b => b.TruckReg).HasMaxLength(50);
            entity.Property(b => b.Status).HasMaxLength(50);
            entity.HasIndex(b => b.PublicId).IsUnique();
        });

        modelBuilder.Entity<Animal>()
            .HasOne(a => a.Booking)
            .WithMany(b => b.Animals)
            .HasForeignKey(a => a.BookingId)
            .OnDelete(DeleteBehavior.Cascade);


        // You can configure more entities here in the future
    }
}
