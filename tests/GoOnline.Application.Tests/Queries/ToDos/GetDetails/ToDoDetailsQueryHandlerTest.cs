using AutoFixture.Xunit2;
using GoOnline.Application.Commands.ToDos.Create;
using GoOnline.Application.Dtos.ToDo;
using GoOnline.Application.Queries.ToDos.GetDetails;
using GoOnline.Domain.Entities;
using GoOnline.Domain.Interfaces;
using MapsterMapper;
using MockQueryable.Moq;
using Moq;

namespace GoOnline.Application.Tests.Queries.ToDos.GetDetails;

public class ToDoDetailsQueryHandlerTest
{
    private readonly Mock<IDataContext> dataContextMock = new();
    private readonly Mock<IMapper> mapperMock = new();
    private readonly ToDoDetailsQueryHandler handler;

    public ToDoDetailsQueryHandlerTest()
    {
        handler = new(dataContextMock.Object, mapperMock.Object);
    }

    [Fact]
    public async Task Handle_WhenQueryPassed_ShoudReturnSuccessResult()
    {
        // Arrange
        var toDo = getToDoQuery().First();
        ToDoDetailsDto dto = new()
        {
            Id = toDo.Id,
            Title = toDo.Title,
            Description = toDo.Description,
            ExpireDate = toDo.ExpireDate,
            Complete = toDo.Complete,
        };
        ToDoDetailsQuery query = new(toDo.Id);
        dataContextMock.Setup(x => x.Set<ToDo>())
            .Returns(getToDoQuery().BuildMockDbSet().Object);
        mapperMock.Setup(x => x.Map<ToDoDetailsDto>(It.IsAny<ToDo>()))
            .Returns(dto);

        // Act
        var result = await handler.Handle(query, default);

        // Assert
        Assert.True(result.Success);
        Assert.Equal(result.Data, dto);
        Assert.Equal(string.Empty, result.Error);

        dataContextMock.Verify(
            x => x.Set<ToDo>(),
            Times.Once);
        mapperMock.Verify(
            x => x.Map<ToDoDetailsDto>(It.IsAny<ToDo>()),
            Times.Once);
    }

    [Theory]
    [AutoData]
    public async Task Handle_ToDoDoesNotExists_ShoudThrowAndReturnFailureResult(string errorMessage)
    {
        // Arrange
        var toDo = getToDoQuery().First();
        ToDoDetailsQuery query = new(toDo.Id);
        dataContextMock.Setup(x => x.Set<ToDo>())
            .Throws(new Exception(errorMessage));

        // Act
        var result = await handler.Handle(query, default);

        // Assert
        Assert.False(result.Success);
        Assert.Null(result.Data);
        Assert.Equal(errorMessage, result.Error);

        dataContextMock.Verify(
            x => x.Set<ToDo>(),
            Times.Once);
        mapperMock.Verify(
            x => x.Map<ToDoDetailsDto>(toDo),
            Times.Never);
    }

    private static IQueryable<ToDo> getToDoQuery()
    {
        return new List<ToDo>()
        {
            new()
            {
                Id = 1,
                Title = "Title",
                Complete = 10m,
                ExpireDate = new(2024, 12, 01, 16, 0, 0),
            },
        }.AsQueryable();
    }
}
