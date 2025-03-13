using GoOnline.Shared.Abstractions;
using MediatR;

namespace GoOnline.Application.Commands.ToDos.Done;

public sealed record ToDoDoneCommand(int id) : IRequest<Result>;
