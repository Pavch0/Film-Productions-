
using FilmFlow.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace FilmFlow.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Actor> Actors { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<Production> Productions { get; set; }
    public DbSet<ProductionMember> ProductionMembers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ProductionMember>()
            .HasKey(pm => new { pm.ProductionId, pm.UserId });

        modelBuilder.Entity<Production>()
            .Property(p => p.Budget)
            .HasPrecision(18, 2);
    }
}
