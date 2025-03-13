using GoOnline.Shared.Abstractions;

namespace GoOnline.Domain.Entities;

public class ToDo : Entity
{
    public string Title { get; set; } = default!;
    public string? Description { get; set; }
    public decimal Complete { get; set; }
    public DateTime ExpireDate { get; set; }
}
