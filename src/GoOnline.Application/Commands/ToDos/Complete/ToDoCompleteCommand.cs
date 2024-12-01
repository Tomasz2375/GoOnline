using GoOnline.Application.Dtos.ToDo;
using GoOnline.Domain.Abstractions;
using MediatR;

namespace GoOnline.Application.Commands.ToDos.Complete;

public sealed record ToDoCompleteCommand(ToDoCompleteDto dto) : IRequest<Result>;
