using GoOnline.Application.Dtos.ToDo;
using GoOnline.Domain.Abstractions;
using MediatR;

namespace GoOnline.Application.Queries.ToDos.GetList;

public sealed record ToDoListQuery() : IRequest<Result<List<ToDoListDto>>>;
