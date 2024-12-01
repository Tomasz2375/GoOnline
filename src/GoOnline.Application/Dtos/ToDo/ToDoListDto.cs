namespace GoOnline.Application.Dtos.ToDo;

public class ToDoListDto : ToDoCompleteDto
{
    public string Title { get; set; } = default!;
    public DateTime ExpireDate { get; set; }
}
