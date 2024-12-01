using GoOnline.Application.Commands.ToDos.Delete;
using GoOnline.Domain.Abstractions;
using GoOnline.Domain.Entities;
using GoOnline.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GO.Application.Commands.ToDos.Delete;

public class ToDoDeleteCommandHandler : IRequestHandler<ToDoDeleteCommand, Result>
{
    private readonly IDataContext dataContext;

    public ToDoDeleteCommandHandler(IDataContext dataContext)
    {
        this.dataContext = dataContext;
    }

    public async Task<Result> Handle(ToDoDeleteCommand command, CancellationToken cancellationToken)
    {
        try
        {
            ToDo toDo = new()
            {
                Id = command.id,
            };

            dataContext.Entry(toDo).State = EntityState.Deleted;

            await dataContext.SaveChangesAsync(cancellationToken);

            return Result.Ok();
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }

    }
}
