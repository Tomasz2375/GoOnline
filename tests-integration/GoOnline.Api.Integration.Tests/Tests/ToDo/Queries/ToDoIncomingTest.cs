using GoOnline.Api.Integration.Tests.Data;
using GoOnline.Application.Dtos.ToDo;
using GoOnline.Domain.Abstractions;
using GoOnline.Domain.Enums;
using Mapster;
using System.Net.Http.Json;

namespace GoOnline.Api.Integration.Tests.Tests.ToDo.Queries;

public class ToDoIncomingTest(GoOnlineWebApplicationFactory factory) : BaseIntegrationTest(factory)
{
    protected override string URL => "todo/incoming";

    [Theory]
    [InlineData(TimePeriod.Today)]
    [InlineData(TimePeriod.Tomorrow)]
    [InlineData(TimePeriod.ThisWeek)]
    [InlineData(TimePeriod.ThisMonth)]
    [InlineData(TimePeriod.ThisYear)]
    public async Task ListToDo_ShouldBeSuccess(TimePeriod timePeriod)
    {
        // Arrange
        var data = getByTimePeriod(ToDoIntegrationFaker.GetAll().AsQueryable(), timePeriod);
        Result<List<ToDoListDto>> expectedResponse = new(true, string.Empty)
        {
            Data = data.Adapt<List<ToDoListDto>>(),
        };

        // Act
        var response = await Client.GetFromJsonAsync<Result<List<ToDoListDto>>>(URL + "?timeperiod=" + (int)timePeriod, default!);

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

    private static IQueryable<Domain.Entities.ToDo> getByTimePeriod(
        IQueryable<Domain.Entities.ToDo> toDos,
        TimePeriod timePeriod)
    {
        switch (timePeriod)
        {
            case TimePeriod.Tomorrow:
                return toDos.Where(x => x.ExpireDate.Date == DateTime.Today.AddDays(1));
            case TimePeriod.ThisWeek:
                var maxDate = DateTime.Today.DayOfWeek == DayOfWeek.Sunday
                    ? DateTime.Today
                    : DateTime.Today.AddDays(7 - (int)DateTime.Today.DayOfWeek);
                return toDos.Where(x => x.ExpireDate.Date >= DateTime.Today && x.ExpireDate.Date <= maxDate);
            case TimePeriod.ThisMonth:
                return toDos.Where(x => x.ExpireDate.Date >= DateTime.Today && x.ExpireDate.Month == DateTime.Today.Month);
            case TimePeriod.ThisYear:
                return toDos.Where(x => x.ExpireDate.Date >= DateTime.Today && x.ExpireDate.Year == DateTime.Today.Year);
            default:
                return toDos.Where(x => x.ExpireDate.Date == DateTime.Today);
        }
    }
}