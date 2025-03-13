using GoOnline.Domain.Entities;
using GoOnline.Domain.Interfaces;
using GoOnline.Shared.Abstractions;
using GoOnline.Shared.Dtos.ToDo;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GoOnline.Application.Queries.ToDos.GetList;

public class ToDoListQueryHandler(IDataContext dataContext, IMapper mapper) : IRequestHandler<ToDoListQuery, Result<List<ToDoListDto>>>
{
    private readonly IDataContext dataContext = dataContext;
    private readonly IMapper mapper = mapper;

    public async Task<Result<List<ToDoListDto>>> Handle(ToDoListQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var toDos = await dataContext.Set<ToDo>()
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            var result = mapper.Map<List<ToDoListDto>>(toDos);

            return Result.Ok(result);
        }
        catch (Exception ex)
        {
            return Result.Fail<List<ToDoListDto>>(ex.Message);
        }
    }
}
