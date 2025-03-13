using GoOnline.Shared.Abstractions;
using GoOnline.Shared.Dtos.ToDo;
using MediatR;

namespace GoOnline.Application.Queries.ToDos.GetDetails;

public sealed record ToDoDetailsQuery(int id) : IRequest<Result<ToDoDetailsDto>>;
