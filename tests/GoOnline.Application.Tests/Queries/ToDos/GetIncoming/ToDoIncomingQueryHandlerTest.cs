﻿using AutoFixture.Xunit2;
using GoOnline.Application.Dtos.ToDo;
using GoOnline.Application.Queries.ToDos.GetIncoming;
using GoOnline.Domain.Entities;
using GoOnline.Domain.Enums;
using GoOnline.Domain.Interfaces;
using MapsterMapper;
using MockQueryable.Moq;
using Moq;

namespace GoOnline.Application.Tests.Queries.ToDos.GetIncoming;

public class ToDoIncomingQueryHandlerTest
{
    private readonly Mock<IDataContext> dataContextMock = new();
    private readonly Mock<IMapper> mapperMock = new();
    private readonly ToDoIncomingQueryHandler handler;

    public ToDoIncomingQueryHandlerTest()
    {
        handler = new(dataContextMock.Object, mapperMock.Object);
    }

    [Fact]
    public async Task Handle_WhenQueryPassed_ShoudReturnSuccessResult()
    {
        // Arrange
        var dtos = getToDoListDtos();
        dataContextMock.Setup(x => x.Set<ToDo>())
            .Returns(getToDoQuery().BuildMockDbSet().Object);
        mapperMock.Setup(x => x.Map<List<ToDoListDto>>(It.IsAny<List<ToDo>>()))
            .Returns(dtos);
        ToDoIncomingQuery query = new(TimePeriod.Tomorrow);

        // Act
        var result = await handler.Handle(query, default);

        // Assert
        Assert.True(result.Success);
        Assert.Equal(result.Data, dtos);
        Assert.Equal(string.Empty, result.Error);

        dataContextMock.Verify(
            x => x.Set<ToDo>(),
            Times.Once);
        mapperMock.Verify(
            x => x.Map<List<ToDoListDto>>(It.IsAny<List<ToDo>>()),
            Times.Once);
    }

    [Theory]
    [AutoData]
    public async Task Handle_WhenQueryDoesNotPassed_ShoudThrowAndReturnFailureResult(string errorMessage)
    {
        // Arrange
        ToDoIncomingQuery query = new(TimePeriod.Tomorrow);
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
            x => x.Map<List<ToDoListDto>>(getToDoQuery()),
            Times.Never);
    }

    private static IQueryable<ToDo> getToDoQuery()
    {
        return new List<ToDo>()
        {
            new()
            {
                Id = 1,
                Title = "Title 1",
                Description = "Description 1",
                Complete = 10m,
                ExpireDate = DateTime.Today.AddDays(1),
            },
            new()
            {
                Id = 2,
                Title = "Title 2",
                Description = "Description 2",
                Complete = 20m,
                ExpireDate = DateTime.Today.AddDays(1),
            },
        }.AsQueryable();
    }

    private static List<ToDoListDto> getToDoListDtos()
    {
        return
        [
            new()
            {
                Id = 1,
                Title = "Title 1",
                Complete = 10m,
                ExpireDate = DateTime.Today.AddDays(1),
            },
            new()
            {
                Id = 2,
                Title = "Title 2",
                Complete = 20m,
                ExpireDate = DateTime.Today.AddDays(1),
            },
        ];
    }
}
