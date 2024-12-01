﻿using GoOnline.Domain.Abstractions;
using GoOnline.Domain.Entities;
using GoOnline.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GoOnline.Application.Commands.ToDos.Done;

public class ToDoDoneCommandHandler : IRequestHandler<ToDoDoneCommand, Result>
{
    private readonly IDataContext dataContext;

    public ToDoDoneCommandHandler(IDataContext dataContext)
    {
        this.dataContext = dataContext;
    }

    public async Task<Result> Handle(ToDoDoneCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var toDo = await dataContext.Set<ToDo>().FirstAsync(x => x.Id == command.id, cancellationToken);

            toDo.Complete = 100;

            await dataContext.SaveChangesAsync(cancellationToken);

            return Result.Ok();
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }
}
