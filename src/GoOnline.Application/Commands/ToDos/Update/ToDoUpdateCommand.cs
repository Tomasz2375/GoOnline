using GoOnline.Application.Dtos.ToDo;
using GoOnline.Domain.Abstractions;
using MediatR;

namespace GoOnline.Application.Commands.ToDos.Update;

public sealed record ToDoUpdateCommand(ToDoDetailsDto toDo) : IRequest<Result>;
