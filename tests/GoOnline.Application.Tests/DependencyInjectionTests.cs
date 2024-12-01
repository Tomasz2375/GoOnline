using GO.Application.Commands.ToDos.Complete;
using GO.Application.Commands.ToDos.Delete;
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
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace GoOnline.Application.Tests;

public class DependencyInjectionTests
{
    [Fact]
    public void AddApplication_ShouldRegisterAppropriateServicesCount()
    {
        // Arrange
        ServiceCollection services = new();

        // Act
        DependencyInjection.AddApplication(services);

        // Assert
        Assert.Equal(16, services.Count);
    }

    [Fact]
    public void AddApplication_ShouldAddExpectedHandlers()
    {
        // Arrange
        ServiceCollection services = new();

        // Act
        DependencyInjection.AddApplication(services);

        // Assert
        // ToDo
        Assert.NotNull(services.FirstOrDefault(x =>
            x.ServiceType == typeof(IRequestHandler<ToDoCompleteCommand, Result>) &&
            x.ImplementationType == typeof(ToDoCompleteCommandHandler) &&
            x.Lifetime == ServiceLifetime.Transient));

        Assert.NotNull(services.FirstOrDefault(x =>
            x.ServiceType == typeof(IRequestHandler<ToDoCreateCommand, Result<int>>) &&
            x.ImplementationType == typeof(ToDoCreateCommandHandler) &&
            x.Lifetime == ServiceLifetime.Transient));

        Assert.NotNull(services.FirstOrDefault(x =>
            x.ServiceType == typeof(IRequestHandler<ToDoDeleteCommand, Result>) &&
            x.ImplementationType == typeof(ToDoDeleteCommandHandler) &&
            x.Lifetime == ServiceLifetime.Transient));

        Assert.NotNull(services.FirstOrDefault(x =>
            x.ServiceType == typeof(IRequestHandler<ToDoDoneCommand, Result>) &&
            x.ImplementationType == typeof(ToDoDoneCommandHandler) &&
            x.Lifetime == ServiceLifetime.Transient));

        Assert.NotNull(services.FirstOrDefault(x =>
            x.ServiceType == typeof(IRequestHandler<ToDoUpdateCommand, Result>) &&
            x.ImplementationType == typeof(ToDoUpdateCommandHandler) &&
            x.Lifetime == ServiceLifetime.Transient));

        Assert.NotNull(services.FirstOrDefault(x =>
            x.ServiceType == typeof(IRequestHandler<ToDoDetailsQuery, Result<ToDoDetailsDto>>) &&
            x.ImplementationType == typeof(ToDoDetailsQueryHandler) &&
            x.Lifetime == ServiceLifetime.Transient));

        Assert.NotNull(services.FirstOrDefault(x =>
            x.ServiceType == typeof(IRequestHandler<ToDoIncomingQuery, Result<List<ToDoListDto>>>) &&
            x.ImplementationType == typeof(ToDoIncomingQueryHandler) &&
            x.Lifetime == ServiceLifetime.Transient));

        Assert.NotNull(services.FirstOrDefault(x =>
            x.ServiceType == typeof(IRequestHandler<ToDoListQuery, Result<List<ToDoListDto>>>) &&
            x.ImplementationType == typeof(ToDoListQueryHandler) &&
            x.Lifetime == ServiceLifetime.Transient));
    }
}