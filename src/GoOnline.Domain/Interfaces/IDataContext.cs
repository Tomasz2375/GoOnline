using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore;

namespace GoOnline.Domain.Interfaces;

public interface IDataContext : IDisposable
{
    EntityEntry Entry(object entity);
    EntityEntry Add(object entities);
    void AddRange(params object[] entities);
    EntityEntry Remove(object entity);
    void RemoveRange(params object[] entities);

    IModel Model { get; }
    DatabaseFacade Database { get; }
    ChangeTracker ChangeTracker { get; }
    Task<int> SaveChangesAsync(CancellationToken token);
    DbSet<TEntity> Set<TEntity>()
        where TEntity : class, IEntity;
}
