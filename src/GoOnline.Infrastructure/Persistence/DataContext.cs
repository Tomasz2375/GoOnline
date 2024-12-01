using GoOnline.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GoOnline.Infrastructure.Persistence;

public class DataContext : DbContext, IDataContext
{
    public DataContext()
    {
    }

    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {
    }

    public new virtual DbSet<TEntity> Set<TEntity>()
        where TEntity : class, IEntity
    {
        return base.Set<TEntity>();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}
