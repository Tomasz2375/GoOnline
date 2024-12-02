using AutoFixture.Xunit2;
using GoOnline.Api.Controllers;
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
using Moq;

namespace GoOnline.Api.Tests.Controllers;

public class ToDoControllerTest
{
    private readonly ToDoController controller;
    private readonly Mock<IMediator> mediatorMock = new();

    public ToDoControllerTest() => controller = new(mediatorMock.Object);

    #region GetToDoDetailsAsync
    [Theory]
    [AutoData]
    public async Task GetToDoDetailsAsync_WnenMediatrReturnsOk_ShouldReturnSuccessResult(ToDoDetailsDto dto)
    {
        // Arrange
        mediatorMock.Setup(x => x.Send(It.IsAny<ToDoDetailsQuery>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(Result.Ok(dto)));

        // Act
        var result = await controller.GetToDoDetailsAsync(dto.Id);

        // Assert
        Assert.True(result.Success);
        Assert.Equal(dto, result.Data);
        Assert.Equal(string.Empty, result.Error);
        mediatorMock.Verify(x => x.Send(new ToDoDetailsQuery(dto.Id), default), Times.Once);
    }

    [Theory]
    [AutoData]
    public async Task GetToDoDetailsAsync_WnenMediatrReturnsFail_ShouldReturnFailureResult(int id, string errorMessage)
    {
        // Arrange
        mediatorMock.Setup(x => x.Send(It.IsAny<ToDoDetailsQuery>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(Result.Fail<ToDoDetailsDto>(errorMessage)));

        // Act
        var result = await controller.GetToDoDetailsAsync(id);

        // Assert
        Assert.False(result.Success);
        Assert.Null(result.Data);
        Assert.Equal(errorMessage, result.Error);
        mediatorMock.Verify(x => x.Send(new ToDoDetailsQuery(id), default), Times.Once);
    }
    #endregion

    #region GetToDosListAsync
    [Theory]
    [AutoData]
    public async Task GetToDosListAsync_WnenMediatrReturnsOk_ShouldReturnSuccessResult(List<ToDoListDto> dtos)
    {
        // Arrange
        mediatorMock.Setup(x => x.Send(It.IsAny<ToDoListQuery>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(Result.Ok(dtos)));

        // Act
        var result = await controller.GetToDosListAsync();

        // Assert
        Assert.True(result.Success);
        Assert.Equal(dtos, result.Data);
        Assert.Equal(string.Empty, result.Error);
        mediatorMock.Verify(x => x.Send(new ToDoListQuery(), default), Times.Once);
    }

    [Theory]
    [AutoData]
    public async Task GetToDosListAsync_WnenMediatrReturnsFail_ShouldReturnFailureResult(string errorMessage)
    {
        // Arrange
        mediatorMock.Setup(x => x.Send(It.IsAny<ToDoListQuery>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(Result.Fail<List<ToDoListDto>>(errorMessage)));

        // Act
        var result = await controller.GetToDosListAsync();

        // Assert
        Assert.False(result.Success);
        Assert.Null(result.Data);
        Assert.Equal(errorMessage, result.Error);
        mediatorMock.Verify(x => x.Send(new ToDoListQuery(), default), Times.Once);
    }
    #endregion

    #region GetToDosIncomingAsync
    [Theory]
    [AutoData]
    public async Task GetToDosIncomingAsync_WnenMediatrReturnsOk_ShouldReturnSuccessResult(TimePeriod timePeriod, List<ToDoListDto> dtos)
    {
        // Arrange
        mediatorMock.Setup(x => x.Send(It.IsAny<ToDoIncomingQuery>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(Result.Ok(dtos)));

        // Act
        var result = await controller.GetToDosIncomingAsync(timePeriod);

        // Assert
        Assert.True(result.Success);
        Assert.Equal(dtos, result.Data);
        Assert.Equal(string.Empty, result.Error);
        mediatorMock.Verify(x => x.Send(new ToDoIncomingQuery(timePeriod), default), Times.Once);
    }

    [Theory]
    [AutoData]
    public async Task GetToDosIncomingAsync_WnenMediatrReturnsFail_ShouldReturnFailureResult(TimePeriod timePeriod, string errorMessage)
    {
        // Arrange
        mediatorMock.Setup(x => x.Send(It.IsAny<ToDoIncomingQuery>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(Result.Fail<List<ToDoListDto>>(errorMessage)));

        // Act
        var result = await controller.GetToDosIncomingAsync(timePeriod);

        // Assert
        Assert.False(result.Success);
        Assert.Null(result.Data);
        Assert.Equal(errorMessage, result.Error);
        mediatorMock.Verify(x => x.Send(new ToDoIncomingQuery(timePeriod), default), Times.Once);
    }
    #endregion

    #region CreateToDoAsync
    [Theory]
    [AutoData]
    public async Task CreateToDoAsync_WnenMediatrReturnsOk_ShouldReturnSuccessResult(ToDoDetailsDto dto, int id)
    {
        // Arrange
        mediatorMock.Setup(x => x.Send(It.IsAny<ToDoCreateCommand>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(Result.Ok(id)));

        // Act
        var result = await controller.CreateToDoAsync(dto);

        // Assert
        Assert.True(result.Success);
        Assert.Equal(id, result.Data);
        Assert.Equal(string.Empty, result.Error);
        mediatorMock.Verify(x => x.Send(new ToDoCreateCommand(dto), default), Times.Once);
    }

    [Theory]
    [AutoData]
    public async Task CreateToDoAsync_WnenMediatrReturnsFail_ShouldReturnFailureResult(ToDoDetailsDto dto, string errorMessage)
    {
        // Arrange
        mediatorMock.Setup(x => x.Send(It.IsAny<ToDoCreateCommand>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(Result.Fail<int>(errorMessage)));

        // Act
        var result = await controller.CreateToDoAsync(dto);

        // Assert
        Assert.False(result.Success);
        Assert.Equal(0, result.Data);
        Assert.Equal(errorMessage, result.Error);
        mediatorMock.Verify(x => x.Send(new ToDoCreateCommand(dto), default), Times.Once);
    }
    #endregion

    #region CompleteToDoAsync
    [Theory]
    [AutoData]
    public async Task CompleteToDoAsync_WnenMediatrReturnsOk_ShouldReturnSuccessResult(ToDoCompleteDto dto)
    {
        // Arrange
        mediatorMock.Setup(x => x.Send(It.IsAny<ToDoCompleteCommand>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(Result.Ok()));

        // Act
        var result = await controller.CompleteToDoAsync(dto);

        // Assert
        Assert.True(result.Success);
        Assert.Equal(string.Empty, result.Error);
        mediatorMock.Verify(x => x.Send(new ToDoCompleteCommand(dto), default), Times.Once);
    }

    [Theory]
    [AutoData]
    public async Task CompleteToDoAsync_WnenMediatrReturnsFail_ShouldReturnFailureResult(ToDoCompleteDto dto, string errorMessage)
    {
        // Arrange
        mediatorMock.Setup(x => x.Send(It.IsAny<ToDoCompleteCommand>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(Result.Fail(errorMessage)));

        // Act
        var result = await controller.CompleteToDoAsync(dto);

        // Assert
        Assert.False(result.Success);
        Assert.Equal(errorMessage, result.Error);
        mediatorMock.Verify(x => x.Send(new ToDoCompleteCommand(dto), default), Times.Once);
    }
    #endregion

    #region DoneToDoAsync
    [Theory]
    [AutoData]
    public async Task DoneToDoAsync_WnenMediatrReturnsOk_ShouldReturnSuccessResult(int id)
    {
        // Arrange
        mediatorMock.Setup(x => x.Send(It.IsAny<ToDoDoneCommand>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(Result.Ok()));

        // Act
        var result = await controller.DoneToDoAsync(id);

        // Assert
        Assert.True(result.Success);
        Assert.Equal(string.Empty, result.Error);
        mediatorMock.Verify(x => x.Send(new ToDoDoneCommand(id), default), Times.Once);
    }

    [Theory]
    [AutoData]
    public async Task DoneToDoAsync_WnenMediatrReturnsFail_ShouldReturnFailureResult(int id, string errorMessage)
    {
        // Arrange
        mediatorMock.Setup(x => x.Send(It.IsAny<ToDoDoneCommand>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(Result.Fail(errorMessage)));

        // Act
        var result = await controller.DoneToDoAsync(id);

        // Assert
        Assert.False(result.Success);
        Assert.Equal(errorMessage, result.Error);
        mediatorMock.Verify(x => x.Send(new ToDoDoneCommand(id), default), Times.Once);
    }
    #endregion

    #region UpdateToDoAsync
    [Theory]
    [AutoData]
    public async Task UpdateToDoAsync_WnenMediatrReturnsOk_ShouldReturnSuccessResult(ToDoDetailsDto dto)
    {
        // Arrange
        mediatorMock.Setup(x => x.Send(It.IsAny<ToDoUpdateCommand>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(Result.Ok()));

        // Act
        var result = await controller.UpdateToDoAsync(dto);

        // Assert
        Assert.True(result.Success);
        Assert.Equal(string.Empty, result.Error);
        mediatorMock.Verify(x => x.Send(new ToDoUpdateCommand(dto), default), Times.Once);
    }

    [Theory]
    [AutoData]
    public async Task UpdateToDoAsync_WnenMediatrReturnsFail_ShouldReturnFailureResult(ToDoDetailsDto dto, string errorMessage)
    {
        // Arrange
        mediatorMock.Setup(x => x.Send(It.IsAny<ToDoUpdateCommand>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(Result.Fail(errorMessage)));

        // Act
        var result = await controller.UpdateToDoAsync(dto);

        // Assert
        Assert.False(result.Success);
        Assert.Equal(errorMessage, result.Error);
        mediatorMock.Verify(x => x.Send(new ToDoUpdateCommand(dto), default), Times.Once);
    }
    #endregion

    #region DeleteToDoAsync
    [Theory]
    [AutoData]
    public async Task DeleteToDoAsync_WnenMediatrReturnsOk_ShouldReturnSuccessResult(int id)
    {
        // Arrange
        mediatorMock.Setup(x => x.Send(It.IsAny<ToDoDeleteCommand>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(Result.Ok()));

        // Act
        var result = await controller.DeleteToDoAsync(id);

        // Assert
        Assert.True(result.Success);
        Assert.Equal(string.Empty, result.Error);
        mediatorMock.Verify(x => x.Send(new ToDoDeleteCommand(id), default), Times.Once);
    }

    [Theory]
    [AutoData]
    public async Task DeleteToDoAsync_WnenMediatrReturnsFail_ShouldReturnFailureResult(int id, string errorMessage)
    {
        // Arrange
        mediatorMock.Setup(x => x.Send(It.IsAny<ToDoDeleteCommand>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(Result.Fail(errorMessage)));

        // Act
        var result = await controller.DeleteToDoAsync(id);

        // Assert
        Assert.False(result.Success);
        Assert.Equal(errorMessage, result.Error);
        mediatorMock.Verify(x => x.Send(new ToDoDeleteCommand(id), default), Times.Once);
    }
    #endregion
}
