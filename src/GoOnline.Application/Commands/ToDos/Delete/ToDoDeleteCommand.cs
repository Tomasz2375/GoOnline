using GoOnline.Domain.Abstractions;
using MediatR;

namespace GoOnline.Application.Commands.ToDos.Delete;

public sealed record ToDoDeleteCommand(int id) : IRequest<Result>;
