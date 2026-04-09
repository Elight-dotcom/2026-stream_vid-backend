using System;

using Microsoft.EntityFrameworkCore;
using StreamVid.Models;

namespace StreamVid.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Movie> Movies { get; set; }
    public DbSet<History> Histories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite("Data Source=app.db");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Table
        modelBuilder.Entity<Movie>().ToTable("Movies");
        modelBuilder.Entity<History>().ToTable("Histories");

        // Relationships
        modelBuilder.Entity<History>()
            .HasOne(h => h.Movie)
            .WithMany(m => m.Histories)
            .HasForeignKey(h => h.MovieId)
            .OnDelete(DeleteBehavior.Cascade);

        // Primary Key
        modelBuilder.Entity<Movie>().HasKey(m => m.Id);
        modelBuilder.Entity<History>().HasKey(h => h.Id);

        // Query filters
        modelBuilder.Entity<Movie>().HasQueryFilter(m => !m.IsDeleted);
        modelBuilder.Entity<History>().HasQueryFilter(h => !h.IsDeleted);

        // Data seeding
        modelBuilder.Entity<Movie>().HasData(
            new Movie { Id = 1, Name = "Zootopia 2", TmdbId = 1084242, FilePath = "C:\\Film\\zootopia2.mp4", IsDeleted = false }
        );
    }

    private void ApplyChanges()
    {
        var now = DateTime.UtcNow;

        foreach (var entry in ChangeTracker.Entries().Where(e => e.Entity is BaseEntity))
        {
            var entity = (BaseEntity)entry.Entity;

            // CreatedAt 
            if (entry.State == EntityState.Added)
            {
                entity.CreatedAt = entity.CreatedAt == default ? now : entity.CreatedAt;
            }

            // UpdatedAt
            if (entry.State == EntityState.Modified)
            {
                entity.UpdatedAt = now;
            }

            // Soft Delete
            if (entry.State == EntityState.Deleted)
            {
                entity.IsDeleted = true;
                entity.DeletedAt = now;
                entry.State = EntityState.Modified;
            }
        }
    }

    public override int SaveChanges()
    {
        ApplyChanges();
        return base.SaveChanges();
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        ApplyChanges();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ApplyChanges();
        return base.SaveChangesAsync(cancellationToken);
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        ApplyChanges();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    public AppDbContext() { }
}