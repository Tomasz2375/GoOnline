using GoOnline.Shared.Abstractions;
using GoOnline.Shared.Dtos.ToDo;
using MediatR;

namespace GoOnline.App.Queries.ToDo.List;

public class ToDoListQueryHandler(HttpClient httpClient)
    : IRequestHandler<ToDoListQuery, Result<List<ToDoListDto>>>
{
    private readonly HttpClient httpClient = httpClient;

    public async Task<Result<List<ToDoListDto>>> Handle(ToDoListQuery request, CancellationToken cancellationToken)
    {
        string baseUrl = "todo/list";

        var response = await httpClient.GetFromJsonAsync<Result<List<ToDoListDto>>>(baseUrl, cancellationToken);

        if (response is null)
        {
            return Result.Fail<List<ToDoListDto>>("Invalid response type");
        }

        return response;
    }
}
