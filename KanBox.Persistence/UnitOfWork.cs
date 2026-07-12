using KanBox.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace KanBox.Persistence;

public class UnitOfWork : IUnitOfWork, IAsyncDisposable
{
    private readonly DbContext _dbContext;

    public UnitOfWork(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateAuditableEntities();
        return _dbContext.SaveChangesAsync(cancellationToken);
    }

    private void UpdateAuditableEntities()
    {
        IEnumerable<EntityEntry<IAuditable>> entries = _dbContext
            .ChangeTracker
            .Entries<IAuditable>();

        foreach (var entry in entries)
        {
            if(entry.State == EntityState.Added)
            {
                entry.Property(auditable => auditable.CreatedOnUtc).CurrentValue = DateTime.UtcNow;
                entry.Property(auditable => auditable.ModifiedOnUtc).CurrentValue = DateTime.UtcNow;
                continue;
            }

            if(entry.State == EntityState.Modified)
            {
                entry.Property(auditable => auditable.ModifiedOnUtc).CurrentValue = DateTime.UtcNow;
            }
        }
    }

    public void Dispose()
    {
        _dbContext.Dispose();
    }

    public async ValueTask DisposeAsync()
    {
        await _dbContext.DisposeAsync();
    }
}
