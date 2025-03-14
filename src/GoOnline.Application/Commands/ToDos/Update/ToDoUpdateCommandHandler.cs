﻿using GoOnline.Domain.Entities;
using GoOnline.Domain.Interfaces;
using GoOnline.Shared.Abstractions;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GoOnline.Application.Commands.ToDos.Update;

public class ToDoUpdateCommandHandler(IDataContext dataContext) : IRequestHandler<ToDoUpdateCommand, Result>
{
    private readonly IDataContext dataContext = dataContext;

    public async Task<Result> Handle(ToDoUpdateCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var toDo = await dataContext.Set<ToDo>()
                .FirstAsync(x => x.Id == command.toDo.Id, cancellationToken);

            command.toDo.Adapt(toDo);

            await dataContext.SaveChangesAsync(cancellationToken);

            return Result.Ok();
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }
}
