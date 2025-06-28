using Microsoft.EntityFrameworkCore;
using RetroSum.Data.Models;

namespace RetroSum.Data.Context;

public class RetroSumContext : DbContext
{
    public DbSet<RetroItem>? Retros { get; set; }

    public RetroSumContext(DbContextOptions<RetroSumContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RetroItem>()
            .Property(r => r.Title)
            .IsRequired();
    }

    public override int SaveChanges()
    {
        AddTimestamps();
        return base.SaveChanges();
    }
    
    private void AddTimestamps()
    {
        var entities = ChangeTracker.Entries()
            .Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));

        foreach (var entity in entities)
        {
            var now = DateTime.UtcNow;

            if (entity.State == EntityState.Added)
            {
                ((BaseEntity)entity.Entity).CreatedAt = now;
            }
            ((BaseEntity)entity.Entity).UpdatedAt = now;
        }
    }
}