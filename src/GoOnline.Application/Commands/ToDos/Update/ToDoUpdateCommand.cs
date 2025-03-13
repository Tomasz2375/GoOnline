using GoOnline.Shared.Abstractions;
using GoOnline.Shared.Dtos.ToDo;
using MediatR;

namespace GoOnline.Application.Commands.ToDos.Update;

public sealed record ToDoUpdateCommand(ToDoDetailsDto toDo) : IRequest<Result>;
