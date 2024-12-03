using GoOnline.Application.Commands.ToDos.Delete;
using GoOnline.Domain.Abstractions;
using GoOnline.Domain.Entities;
using GoOnline.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GO.Application.Commands.ToDos.Delete;

public class ToDoDeleteCommandHandler(IDataContext dataContext) : IRequestHandler<ToDoDeleteCommand, Result>
{
    private readonly IDataContext dataContext = dataContext;

    public async Task<Result> Handle(ToDoDeleteCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var toDo = await dataContext
                .Set<ToDo>()
                .FirstAsync(x => x.Id == command.id, cancellationToken);

            dataContext.Remove(toDo);

            await dataContext.SaveChangesAsync(cancellationToken);

            return Result.Ok();
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }
}
