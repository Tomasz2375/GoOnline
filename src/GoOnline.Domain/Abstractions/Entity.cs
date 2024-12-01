using GoOnline.Domain.Interfaces;

namespace GoOnline.Domain.Abstractions;

public abstract class Entity : IEntity
{
    public int Id { get; set; }
}
