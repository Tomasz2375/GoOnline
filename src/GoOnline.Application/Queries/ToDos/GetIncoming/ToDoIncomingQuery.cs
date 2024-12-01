using GoOnline.Application.Dtos.ToDo;
using GoOnline.Domain.Abstractions;
using GoOnline.Domain.Enums;
using MediatR;

namespace GoOnline.Application.Queries.ToDos.GetIncoming;

public sealed record ToDoIncomingQuery(TimePeriod timePeriod) : IRequest<Result<List<ToDoListDto>>>;
