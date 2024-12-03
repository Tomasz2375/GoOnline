using GoOnline.Api.Integration.Tests.Data;
using GoOnline.Application.Dtos.ToDo;
using GoOnline.Domain.Abstractions;
using System.Net.Http.Json;

namespace GoOnline.Api.Integration.Tests.Tests.ToDo.Commands;

public class ToDoCompleteTest(GoOnlineWebApplicationFactory factory) : BaseIntegrationTest(factory)
{
    protected override string URL => "todo/complete";

    [Fact]
    public async Task CompleteToDo_WhenAllDataIsValid_ShouldBeSuccess()
    {
        // Arrange
        ToDoCompleteDto dto = new()
        {
            Id = ToDoIntegrationFaker.GetAll().First().Id,
            Complete = 99.9m,
        };
        Result expectedResponse = new(true, string.Empty);

        // Act
        var result = await Client.PutAsJsonAsync(URL, dto, default);
        var response = await result.Content.ReadFromJsonAsync<Result>();

        // Assert
        Assert.True(response!.Success);
        Assert.Equal(string.Empty, response.Error);
    }

    [Fact]
    public async Task CompleteToDo_WhenCompleteHasInvalidValue_ShouldReturnErrorResponse()
    {
        // Arrange
        ToDoCompleteDto dto = new()
        {
            Id = ToDoIntegrationFaker.GetAll().First().Id,
            Complete = 100.9m,
        };
        ErrorResponse expectedResponse = new()
        {
            Status = 400,
            Errors = new Dictionary<string, string[]>
            {
                { "Complete", new[] { "'Complete' must be between 0 and 100. You entered 100,9." } },
            },
        };

        // Act
        var result = await Client.PutAsJsonAsync(URL, dto, default);
        var response = await result.Content.ReadFromJsonAsync<ErrorResponse>();

        // Assert
        Assert.False(response!.Success);
        Assert.Equal(expectedResponse.Status, response.Status);
        Assert.Equal(expectedResponse.Errors, response.Errors);
    }
}
