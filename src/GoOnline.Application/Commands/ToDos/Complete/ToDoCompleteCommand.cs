using GoOnline.Shared.Abstractions;
using GoOnline.Shared.Dtos.ToDo;
using MediatR;

namespace GoOnline.Shared.Commands.ToDos.Complete;

public sealed record ToDoCompleteCommand(ToDoCompleteDto dto) : IRequest<Result>;
