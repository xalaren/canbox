using KanBox.Domain.Priorities;
using Microsoft.EntityFrameworkCore;

namespace KanBox.Persistence;

public class KanBoxDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Priority> Priorities { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new PriorityEntityTypeConfiguration());
        
        base.OnModelCreating(modelBuilder);
    }
}
