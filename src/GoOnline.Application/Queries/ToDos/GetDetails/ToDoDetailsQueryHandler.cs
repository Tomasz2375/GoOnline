using GoOnline.Application.Dtos.ToDo;
using GoOnline.Domain.Abstractions;
using GoOnline.Domain.Entities;
using GoOnline.Domain.Interfaces;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GoOnline.Application.Queries.ToDos.GetDetails;

public class ToDoDetailsQueryHandler : IRequestHandler<ToDoDetailsQuery, Result<ToDoDetailsDto>>
{
    private readonly IDataContext dataContext;
    private readonly IMapper mapper;

    public ToDoDetailsQueryHandler(IDataContext dataContext, IMapper mapper)
    {
        this.dataContext = dataContext;
        this.mapper = mapper;
    }

    public async Task<Result<ToDoDetailsDto>> Handle(ToDoDetailsQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var toDo = await dataContext.Set<ToDo>()
                .AsNoTracking()
                .FirstAsync(x => x.Id == query.id, cancellationToken);

            var result = mapper.Map<ToDoDetailsDto>(toDo);

            return Result.Ok(result!);

        }
        catch (Exception ex)
        {
            return Result.Fail<ToDoDetailsDto>(ex.Message);
        }
    }
}
