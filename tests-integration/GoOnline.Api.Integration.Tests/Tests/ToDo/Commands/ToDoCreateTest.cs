﻿using GoOnline.Application.Dtos.ToDo;
using GoOnline.Domain.Abstractions;
using System.Net.Http.Json;

namespace GoOnline.Api.Integration.Tests.Tests.ToDo.Commands;

public class ToDoCreateTest(GoOnlineWebApplicationFactory factory) : BaseIntegrationTest(factory)
{
    protected override string URL => "todo/create";

    [Fact]
    public async Task CreateToDo_WhenAllDataIsValid_ShouldBeSuccess()
    {
        // Arrange
        var dto = validDto();
        Result<int> expectedResponse = new(true, string.Empty)
        {
            Data = 1007,
        };

        // Act
        var result = await Client.PostAsJsonAsync(URL, dto, default);
        var response = await result.Content.ReadFromJsonAsync<Result<int>>();

        // Assert
        Assert.True(response!.Success);
        Assert.Equal(expectedResponse.Data, response.Data);
        Assert.Equal(string.Empty, response.Error);
    }

    [Fact]
    public async Task CreateToDo_WhenTitleIsEmpty_ShouldReturnErrorResponse()
    {
        // Arrange
        var dto = validDto();
        dto.Title = string.Empty;
        ErrorResponse expectedResponse = new()
        {
            Status = 400,
            Errors = new Dictionary<string, string[]>
            {
                { "Title", new[] { "'Title' must not be empty." } },
            },
        };

        // Act
        var result = await Client.PostAsJsonAsync(URL, dto, default);
        var response = await result.Content.ReadFromJsonAsync<ErrorResponse>();

        // Assert
        Assert.False(response!.Success);
        Assert.Equal(expectedResponse.Status, response.Status);
        Assert.Equal(expectedResponse.Errors, response.Errors);
    }


    [Fact]
    public async Task CreateToDo_WhenTitleIsToLong_ShouldReturnErrorResponse()
    {
        // Arrange
        var dto = validDto();
        dto.Title = string.Concat(Enumerable.Repeat(".", 101));
        ErrorResponse expectedResponse = new()
        {
            Status = 400,
            Errors = new Dictionary<string, string[]>
            {
                { "Title", new[] { "The length of 'Title' must be 100 characters or fewer. You entered 101 characters." } },
            },
        };

        // Act
        var result = await Client.PostAsJsonAsync(URL, dto, default);
        var response = await result.Content.ReadFromJsonAsync<ErrorResponse>();

        // Assert
        Assert.False(response!.Success);
        Assert.Equal(expectedResponse.Status, response.Status);
        Assert.Equal(expectedResponse.Errors, response.Errors);
    }


    [Fact]
    public async Task CreateToDo_WhenCompletIsNotValid_ShouldReturnErrorResponse()
    {
        // Arrange
        var dto = validDto();
        dto.Complete = -1;
        ErrorResponse expectedResponse = new()
        {
            Status = 400,
            Errors = new Dictionary<string, string[]>
            {
                { "Complete", new[] { "'Complete' must be between 0 and 100. You entered -1." } },
            },
        };

        // Act
        var result = await Client.PostAsJsonAsync(URL, dto, default);
        var response = await result.Content.ReadFromJsonAsync<ErrorResponse>();

        // Assert
        Assert.False(response!.Success);
        Assert.Equal(expectedResponse.Status, response.Status);
        Assert.Equal(expectedResponse.Errors, response.Errors);
    }

    private static ToDoDetailsDto validDto() => new()
    {
        Title = "Titile 1",
        Description = "Description 1",
        ExpireDate = new DateTime(2024, 12, 12),
        Complete = 10.5m,
    };
}