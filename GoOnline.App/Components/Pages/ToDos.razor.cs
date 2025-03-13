using System.Threading.Tasks;
using GoOnline.App.Queries.ToDo.List;
using GoOnline.Shared.Dtos.ToDo;

namespace GoOnline.App.Components.Pages;

public partial class ToDos
{
    public List<ToDoListDto> Model { get; set; } = new List<ToDoListDto>();

    protected override async Task OnInitializedAsync()
    {
        var result = await Mediator.Send(new ToDoListQuery());

        if (!result.Success)
        {
            return;
        }

        Model = result.Data;
    }
}
