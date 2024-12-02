using AutoFixture.Xunit2;
using GoOnline.Application.Commands.ToDos.Complete;
using GoOnline.Application.Commands.ToDos.Create;
using GoOnline.Application.Dtos.ToDo;
using GoOnline.Domain.Entities;
using GoOnline.Domain.Interfaces;
using MapsterMapper;
using Moq;

namespace GoOnline.Application.Tests.Commands.ToDos.Create;

public class ToDoCreateCommandHandlerTest
{
    private readonly Mock<IDataContext> dataContextMock = new();
    private readonly Mock<IMapper> mapperMock = new();
    private readonly ToDoCreateCommandHandler handler;

    public ToDoCreateCommandHandlerTest()
    {
        handler = new(dataContextMock.Object, mapperMock.Object);
    }

    [Theory]
    [AutoData]
    public async Task Handle_WhenCommandPassed_ShoudReturnSuccessResult(ToDoDetailsDto dto)
    {
        // Arrange
        ToDo toDo = new()
        {
            Id = 1,
            Title = dto.Title,
            Description = dto.Description,
            ExpireDate = dto.ExpireDate,
            Complete = dto.Complete,
        };
        ToDoCreateCommand command = new(dto);
        mapperMock.Setup(x => x.Map<ToDo>(It.IsAny<ToDoDetailsDto>()))
            .Returns(toDo);

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        Assert.True(result.Success);
        Assert.Equal(result.Data, toDo.Id);
        Assert.Equal(string.Empty, result.Error);

        mapperMock.Verify(
            x => x.Map<ToDo>(dto),
            Times.Once());
        dataContextMock.Verify(
            x => x.SaveChangesAsync(default),
            Times.Once());
    }

    [Theory]
    [AutoData]
    public async Task Handle_WhenSaveChangesFails_ShoudThrowAndReturnFailureResult(ToDoDetailsDto dto, string errorMessage)
    {
        // Arrange
        ToDo toDo = new()
        {
            Id = 1,
            Title = dto.Title,
            Description = dto.Description,
            ExpireDate = dto.ExpireDate,
            Complete = dto.Complete,
        };
        ToDoCreateCommand command = new(dto);
        mapperMock.Setup(x => x.Map<ToDo>(It.IsAny<ToDoDetailsDto>()))
            .Returns(toDo);
        dataContextMock.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception(errorMessage));

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        Assert.False(result.Success);
        Assert.Equal(0, result.Data);
        Assert.Equal(errorMessage, result.Error);

        mapperMock.Verify(
            x => x.Map<ToDo>(dto),
            Times.Once());
        dataContextMock.Verify(
            x => x.SaveChangesAsync(default),
            Times.Once);
    }
}
