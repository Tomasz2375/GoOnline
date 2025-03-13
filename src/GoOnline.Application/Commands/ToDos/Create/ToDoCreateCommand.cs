using GoOnline.Shared.Abstractions;
using GoOnline.Shared.Dtos.ToDo;
using MediatR;

namespace GoOnline.Shared.Commands.ToDos.Create;

public sealed record ToDoCreateCommand(ToDoDetailsDto dto) : IRequest<Result<int>>;
