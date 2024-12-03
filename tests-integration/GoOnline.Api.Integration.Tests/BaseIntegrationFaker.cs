using GoOnline.Domain.Interfaces;

namespace GoOnline.Api.Integration.Tests;

public abstract class BaseIntegrationFaker<TEntity>
    where TEntity : class, IEntity
{
    protected static int BASE_ID { get; set; } = 1000;
}
