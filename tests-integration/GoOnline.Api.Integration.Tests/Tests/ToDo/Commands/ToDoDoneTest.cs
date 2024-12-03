using GoOnline.Api.Integration.Tests.Data;
using GoOnline.Domain.Abstractions;
using System.Net.Http.Json;

namespace GoOnline.Api.Integration.Tests.Tests.ToDo.Commands;

public class ToDoDoneTest(GoOnlineWebApplicationFactory factory) : BaseIntegrationTest(factory)
{
    protected override string URL => "todo/done";

    [Fact]
    public async Task DoneToDo_WhenToDoExist_ShouldBeSuccess()
    {
        // Arrange
        var id = ToDoIntegrationFaker.GetAll().Skip(1).First().Id;
        Result expectedResponse = new(true, string.Empty);

        // Act
        var result = await Client.PutAsync(URL + "/" + id, null, default);
        var response = await result.Content.ReadFromJsonAsync<Result>();

        // Assert
        Assert.True(response!.Success);
        Assert.Equal(string.Empty, response.Error);
    }

    [Fact]
    public async Task DoneToDo_WhenToDoDoesNotExist_ShouldReturnErrorResult()
    {
        // Arrange
        var id = 999;
        Result expectedResponse = new(false, "Sequence contains no elements.");

        // Act
        var result = await Client.PutAsync(URL + "/" + id, null, default);
        var response = await result.Content.ReadFromJsonAsync<Result>();

        // Assert
        Assert.False(response!.Success);
        Assert.Equal(expectedResponse.Error, response.Error);
    }
}
