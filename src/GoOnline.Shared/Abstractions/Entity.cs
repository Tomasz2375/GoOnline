using GoOnline.Shared.Interfaces;

namespace GoOnline.Shared.Abstractions;

public abstract class Entity : IEntity
{
    public int Id { get; set; }
}
