using GoOnline.Api.Integration.Tests.Data;
using GoOnline.Application.Dtos.ToDo;
using GoOnline.Domain.Abstractions;
using Mapster;
using System.Net.Http.Json;

namespace GoOnline.Api.Integration.Tests.Tests.ToDo.Queries;

public class ToDoListTest(GoOnlineWebApplicationFactory factory) : BaseIntegrationTest(factory)
{
    protected override string URL => "todo/list";

    [Fact]
    public async Task ListToDo_ShouldBeSuccess()
    {
        // Arrange
        Result<List<ToDoListDto>> expectedResponse = new(true, string.Empty)
        {
            Data = ToDoIntegrationFaker.GetAll().Adapt<List<ToDoListDto>>(),
        };

        // Act
        var response = await Client.GetFromJsonAsync<Result<List<ToDoListDto>>>(URL, default!);

        // Assert
        Assert.True(response!.Success);
        Assert.Equal(string.Empty, response.Error);
        Assert.All(response.Data, item =>
        {
            var expectedItem = expectedResponse.Data.First(e => e.Id == item.Id);
            Assert.Equal(expectedItem.Title, item.Title);
            Assert.Equal(expectedItem.Complete, item.Complete);
            Assert.Equal(expectedItem.ExpireDate, item.ExpireDate);
        });
    }
}
