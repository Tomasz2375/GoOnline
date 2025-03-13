namespace GoOnline.Shared.Dtos.ToDo;

public class ToDoDetailsDto : ToDoCompleteDto
{
    public string Title { get; set; } = default!;
    public DateTime ExpireDate { get; set; }
    public string? Description { get; set; }
}
