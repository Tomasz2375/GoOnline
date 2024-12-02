using GoOnline.Application.Commands.ToDos.Complete;
using GoOnline.Domain.Abstractions;
using GoOnline.Domain.Entities;
using GoOnline.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GO.Application.Commands.ToDos.Complete;

public class ToDoCompleteCommandHandler(IDataContext dataContext) : IRequestHandler<ToDoCompleteCommand, Result>
{
    private readonly IDataContext dataContext = dataContext;

    public async Task<Result> Handle(ToDoCompleteCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var toDo = await dataContext
                .Set<ToDo>()
                .FirstAsync(x => x.Id == command.dto.Id, cancellationToken);

            toDo.Complete = command.dto.Complete;

            await dataContext.SaveChangesAsync(cancellationToken);

            return Result.Ok();
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }
}
