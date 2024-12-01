using GoOnline.Application.Dtos.ToDo;
using GoOnline.Domain.Abstractions;
using MediatR;

namespace GoOnline.Application.Commands.ToDos.Create;

public sealed record ToDoCreateCommand(ToDoDetailsDto dto) : IRequest<Result<int>>;
