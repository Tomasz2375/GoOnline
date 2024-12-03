using GoOnline.Api.Integration.Tests.Data;
using GoOnline.Application.Dtos.ToDo;
using GoOnline.Domain.Abstractions;
using Mapster;
using System.Net.Http.Json;

namespace GoOnline.Api.Integration.Tests.Tests.ToDo.Queries;

public class ToDoDetailsTest(GoOnlineWebApplicationFactory factory) : BaseIntegrationTest(factory)
{
    protected override string URL => "todo/details";

    [Fact]
    public async Task DetailsToDo_WhenToDoExists_ShouldBeSuccess()
    {
        // Arrange
        var id = ToDoIntegrationFaker.GetAll().Skip(3).First().Id;
        Result<ToDoDetailsDto> expectedResponse = new(true, string.Empty)
        {
            Data = ToDoIntegrationFaker.GetAll().First(x => x.Id == id).Adapt<ToDoDetailsDto>(),
        };

        // Act
        var response = await Client.GetFromJsonAsync<Result<ToDoDetailsDto>>(URL + "/" + id, default!);

        // Assert
        Assert.True(response!.Success);
        Assert.Equal(string.Empty, response.Error);
        Assert.Equal(expectedResponse.Data.Id, response.Data.Id);
        Assert.Equal(expectedResponse.Data.Title, response.Data.Title);
        Assert.Equal(expectedResponse.Data.Description, response.Data.Description);
        Assert.Equal(expectedResponse.Data.Complete, response.Data.Complete);
        Assert.Equal(expectedResponse.Data.ExpireDate, response.Data.ExpireDate);
    }

    [Fact]
    public async Task DetailsToDo_WhenToDoDoesNotExists_ShouldReturnErrorWithExpectedMessage()
    {
        // Arrange
        var id = 999;
        Result<ToDoDetailsDto> expectedResponse = new(false, "Sequence contains no elements.")
        {
            Data = null!,
        };

        // Act
        var response = await Client.GetFromJsonAsync<Result<ToDoDetailsDto>>(URL + "/" + id, default!);

        // Assert
        Assert.False(response!.Success);
        Assert.Equal(expectedResponse.Error, response.Error);
        Assert.Equal(expectedResponse.Data, response.Data);
    }
}
