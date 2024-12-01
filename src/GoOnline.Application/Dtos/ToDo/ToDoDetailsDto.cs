namespace GoOnline.Application.Dtos.ToDo;

public class ToDoDetailsDto : ToDoCompleteDto
{
    public string Title { get; set; } = default!;
    public DateTime ExpireDate { get; set; }
    public string? Description { get; set; }
}
