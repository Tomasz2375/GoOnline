using AutoFixture.Xunit2;
using GO.Application.Commands.ToDos.Complete;
using GoOnline.Application.Commands.ToDos.Complete;
using GoOnline.Application.Dtos.ToDo;
using GoOnline.Domain.Entities;
using GoOnline.Domain.Interfaces;
using MockQueryable.Moq;
using Moq;

namespace GoOnline.Application.Tests.Commands.ToDos.Complete;

public class ToDoCompleteCommandHandlerTest
{
    private readonly Mock<IDataContext> dataContextMock = new();
    private readonly ToDoCompleteCommandHandler handler;

    public ToDoCompleteCommandHandlerTest()
    {
        handler = new(dataContextMock.Object);
    }

    [Fact]
    public async Task Handle_WhenCommandPassed_ShoudReturnSuccessResult()
    {
        // Arrange
        var toDoId = getToDoQuery().First().Id;
        ToDoCompleteDto dto = new()
        {
            Id = toDoId,
            Complete = 33.33m
        };
        ToDoCompleteCommand command = new(dto);
        dataContextMock.Setup(x => x.Set<ToDo>())
            .Returns(getToDoQuery().BuildMockDbSet().Object);

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        Assert.True(result.Success);
        Assert.Equal(string.Empty, result.Error);

        dataContextMock.Verify(
            x => x.Set<ToDo>(),
            Times.Once());
        dataContextMock.Verify(
            x => x.SaveChangesAsync(default),
            Times.Once());
    }

    [Theory]
    [AutoData]
    public async Task Handle_WhenToDoDoesNotExists_ShoudThrowAndReturnFailureResult(string errorMessage)
    {
        // Arrange
        ToDoCompleteDto dto = new()
        {
            Id = 999,
            Complete = 33.33m
        };
        ToDoCompleteCommand command = new(dto);
        dataContextMock.Setup(x => x.Set<ToDo>())
            .Throws(new Exception(errorMessage));

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        Assert.False(result.Success);
        Assert.Equal(errorMessage, result.Error);

        dataContextMock.Verify(
            x => x.Set<ToDo>(),
            Times.Once());
        dataContextMock.Verify(
            x => x.SaveChangesAsync(default),
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
