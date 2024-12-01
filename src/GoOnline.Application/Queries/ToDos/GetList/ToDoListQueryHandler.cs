using GoOnline.Application.Dtos.ToDo;
using GoOnline.Domain.Abstractions;
using GoOnline.Domain.Entities;
using GoOnline.Domain.Interfaces;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GoOnline.Application.Queries.ToDos.GetList;

public class ToDoListQueryHandler : IRequestHandler<ToDoListQuery, Result<List<ToDoListDto>>>
{
    private readonly IDataContext dataContext;
    private readonly IMapper mapper;

    public ToDoListQueryHandler(IDataContext dataContext, IMapper mapper)
    {
        this.dataContext = dataContext;
        this.mapper = mapper;
    }

    public async Task<Result<List<ToDoListDto>>> Handle(ToDoListQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var toDosQuery = dataContext.Set<ToDo>()
                .AsNoTracking();

            var toDos = await toDosQuery.ToListAsync(cancellationToken);
            var result = mapper.Map<List<ToDoListDto>>(toDos);

            return Result.Ok(result);
        }
        catch (Exception ex)
        {
            return Result.Fail<List<ToDoListDto>>(ex.Message);
        }
    }
}
