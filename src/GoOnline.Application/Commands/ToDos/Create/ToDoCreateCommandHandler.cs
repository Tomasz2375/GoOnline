using GoOnline.Domain.Abstractions;
using GoOnline.Domain.Entities;
using GoOnline.Domain.Interfaces;
using MapsterMapper;
using MediatR;

namespace GoOnline.Application.Commands.ToDos.Create;

public class ToDoCreateCommandHandler(IDataContext dataContext, IMapper mapper) : IRequestHandler<ToDoCreateCommand, Result<int>>
{
    private readonly IDataContext dataContext = dataContext;
    private readonly IMapper mapper = mapper;

    public async Task<Result<int>> Handle(ToDoCreateCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var toDo = mapper.Map<ToDo>(command.dto);

            dataContext.Add(toDo);

            await dataContext.SaveChangesAsync(cancellationToken);

            return Result.Ok(toDo.Id);
        }
        catch (Exception ex)
        {
            return Result.Fail<int>(ex.Message);
        }
    }
}
