using GoOnline.Shared.Abstractions;
using GoOnline.Shared.Dtos.ToDo;
using MediatR;

namespace GoOnline.App.Queries.ToDo.List;

public sealed record ToDoListQuery : IRequest<Result<List<ToDoListDto>>>
{
}
