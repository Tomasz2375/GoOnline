using GoOnline.Application.Commands.ToDos.Complete;
using GoOnline.Application.Commands.ToDos.Create;
using GoOnline.Application.Commands.ToDos.Delete;
using GoOnline.Application.Commands.ToDos.Done;
using GoOnline.Application.Commands.ToDos.Update;
using GoOnline.Application.Dtos.ToDo;
using GoOnline.Application.Queries.ToDos.GetDetails;
using GoOnline.Application.Queries.ToDos.GetIncoming;
using GoOnline.Application.Queries.ToDos.GetList;
using GoOnline.Domain.Abstractions;
using GoOnline.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GoOnline.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ToDoController(IMediator mediator) : ControllerBase
{
    private readonly IMediator mediator = mediator;

    [HttpGet("details/{id}")]
    public async Task<Result<ToDoDetailsDto>> GetToDoDetailsAsync(int id)
    {
        return await mediator.Send(new ToDoDetailsQuery(id));
    }

    [HttpGet("list")]
    public async Task<Result<List<ToDoListDto>>> GetToDosListAsync()
    {
        return await mediator.Send(new ToDoListQuery());
    }

    [HttpGet("incoming")]
    public async Task<Result<List<ToDoListDto>>> GetToDosIncomingAsync(TimePeriod timePeriod)
    {
        return await mediator.Send(new ToDoIncomingQuery(timePeriod));
    }

    [HttpPost("create")]
    public async Task<Result<int>> CreateToDoAsync(ToDoDetailsDto dto)
    {
        return await mediator.Send(new ToDoCreateCommand(dto));
    }

    [HttpPut("complete")]
    public async Task<Result> CompleteToDoAsync(ToDoCompleteDto dto)
    {
        return await mediator.Send(new ToDoCompleteCommand(dto));
    }

    [HttpPut("done/{id}")]
    public async Task<Result> DoneToDoAsync(int id)
    {
        return await mediator.Send(new ToDoDoneCommand(id));
    }

    [HttpPut("update")]
    public async Task<Result> UpdateToDoAsync(ToDoDetailsDto dto)
    {
        return await mediator.Send(new ToDoUpdateCommand(dto));
    }

    [HttpDelete("delete/{id}")]
    public async Task<Result> DeleteToDoAsync(int id)
    {
        return await mediator.Send(new ToDoDeleteCommand(id));
    }
}
