using GoOnline.Shared.Abstractions;
using GoOnline.Shared.Dtos.ToDo;
using MediatR;

namespace GoOnline.Application.Queries.ToDos.GetList;

public sealed record ToDoListQuery() : IRequest<Result<List<ToDoListDto>>>;
