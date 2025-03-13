using GoOnline.Domain.Entities;
using GoOnline.Domain.Interfaces;
using GoOnline.Shared.Abstractions;
using GoOnline.Shared.Dtos.ToDo;
using GoOnline.Shared.Enums;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GoOnline.Application.Queries.ToDos.GetIncoming;

public class ToDoIncomingQueryHandler(IDataContext dataContext, IMapper mapper) : IRequestHandler<ToDoIncomingQuery, Result<List<ToDoListDto>>>
{
    private readonly IDataContext dataContext = dataContext;
    private readonly IMapper mapper = mapper;

    public async Task<Result<List<ToDoListDto>>> Handle(ToDoIncomingQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var toDosQuery = dataContext.Set<ToDo>()
                .AsNoTracking();

            toDosQuery = getByTimePeriod(toDosQuery, query);

            var toDos = await toDosQuery.ToListAsync(cancellationToken);
            var result = mapper.Map<List<ToDoListDto>>(toDos);

            return Result.Ok(result);
        }
        catch (Exception ex)
        {
            return Result.Fail<List<ToDoListDto>>(ex.Message);
        }
    }

    private static IQueryable<ToDo> getByTimePeriod(
        IQueryable<ToDo> toDos,
        ToDoIncomingQuery query)
    {
        switch (query.timePeriod)
        {
            case TimePeriod.Tomorrow:
                return toDos.Where(x => x.ExpireDate.Date == DateTime.Today.AddDays(1));
            case TimePeriod.ThisWeek:
                var maxDate = DateTime.Today.DayOfWeek == DayOfWeek.Sunday
                    ? DateTime.Today
                    : DateTime.Today.AddDays(7 - (int)DateTime.Today.DayOfWeek);
                return toDos.Where(x => x.ExpireDate.Date >= DateTime.Today && x.ExpireDate.Date <= maxDate);
            case TimePeriod.ThisMonth:
                return toDos.Where(x => x.ExpireDate.Date >= DateTime.Today && x.ExpireDate.Month == DateTime.Today.Month);
            case TimePeriod.ThisYear:
                return toDos.Where(x => x.ExpireDate.Date >= DateTime.Today && x.ExpireDate.Year == DateTime.Today.Year);
            default:
                return toDos.Where(x => x.ExpireDate.Date == DateTime.Today);
        }
    }
}
