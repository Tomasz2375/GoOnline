using GoOnline.Application.Dtos.ToDo;
using GoOnline.Domain.Abstractions;
using MediatR;

namespace GoOnline.Application.Queries.ToDos.GetDetails;

public sealed record ToDoDetailsQuery(int id) : IRequest<Result<ToDoDetailsDto>>;
