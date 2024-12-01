using GoOnline.Domain.Interfaces;
using GoOnline.Infrastructure.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace GoOnline.Infrastructure.Tests;

public class DependencyInjectionTests
{
    [Fact]
    public void AddInfrastructure_ShouldRegisterAppropriateServicesCount()
    {
        // Arrange
        Mock<IConfiguration> configurationMock = new();
        ServiceCollection services = new();

        // Act
        DependencyInjection.AddInfrastructure(services, configurationMock.Object);

        // Assert
        Assert.Equal(5, services.Count);
    }

    [Fact]
    public void AddInfrastructure_ShouldAddExpectedServices()
    {
        // Arrange
        Mock<IConfiguration> configurationMock = new();
        ServiceCollection services = new();

        // Act
        DependencyInjection.AddInfrastructure(services, configurationMock.Object);

        // Assert
        Assert.NotNull(services.FirstOrDefault(x =>
            x.ServiceType == typeof(IDataContext) &&
            x.ImplementationType == typeof(DataContext) &&
            x.Lifetime == ServiceLifetime.Transient));
    }
}