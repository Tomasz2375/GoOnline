using System.Reflection;

namespace GoOnline.Architecture.Tests;

public class ArchitectureTest
{
    private const string DOMAIN_NAMESPACE = "GoOnline.Domain";
    private const string APPLICATION_NAMESPACE = "GoOnline.Application";
    private const string INFRASTRUCTURE_NAMESPACE = "GoOnline.Infrastructure";
    private const string API_NAMESPACE = "GoOnline.Api";

    [Fact]
    public void Domain_ShouldNotHaveDependencyOnOtherProject()
    {
        // Arrange
        var assembly = Assembly.Load(DOMAIN_NAMESPACE);
        var assemblyName = assembly.GetReferencedAssemblies();

        var otherProjects = new[]
        {
            APPLICATION_NAMESPACE,
            INFRASTRUCTURE_NAMESPACE,
            API_NAMESPACE,
        };

        // Act
        var result = otherProjects.Any(x => assemblyName.Select(y => y.Name).Contains(x));

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Application_ShouldNotHaveDependencyOnOtherProject()
    {
        // Arrange
        var assembly = Assembly.Load(APPLICATION_NAMESPACE);
        var assemblyName = assembly.GetReferencedAssemblies();

        var otherProjects = new[]
        {
            INFRASTRUCTURE_NAMESPACE,
            API_NAMESPACE,
        };

        // Act
        var result = otherProjects.Any(x => assemblyName.Select(y => y.Name).Contains(x));

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Infrastructure_ShouldNotHaveDependencyOnOtherProject()
    {
        // Arrange
        var assembly = Assembly.Load(INFRASTRUCTURE_NAMESPACE);
        var assemblyName = assembly.GetReferencedAssemblies();

        var otherProjects = new[]
        {
            API_NAMESPACE,
        };

        // Act
        var result = otherProjects.Any(x => assemblyName.Select(y => y.Name).Contains(x));

        // Assert
        Assert.False(result);
    }
}