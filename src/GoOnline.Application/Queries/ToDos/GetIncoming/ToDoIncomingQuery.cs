using GoOnline.Shared.Abstractions;
using GoOnline.Shared.Dtos.ToDo;
using GoOnline.Shared.Enums;
using MediatR;

namespace GoOnline.Application.Queries.ToDos.GetIncoming;

public sealed record ToDoIncomingQuery(TimePeriod timePeriod) : IRequest<Result<List<ToDoListDto>>>;
