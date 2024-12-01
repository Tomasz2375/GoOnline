using GoOnline.Domain.Abstractions;
using MediatR;

namespace GoOnline.Application.Commands.ToDos.Done;

public sealed record ToDoDoneCommand(int id) : IRequest<Result>;
