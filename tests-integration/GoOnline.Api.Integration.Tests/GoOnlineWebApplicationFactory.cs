using GoOnline.Domain.Interfaces;
using GoOnline.Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Data.Common;
using Testcontainers.MsSql;

namespace GoOnline.Api.Integration.Tests;

public class GoOnlineWebApplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly MsSqlContainer msSqlContainer = new MsSqlBuilder()
        .WithImage("mcr.microsoft.com/mssql/server:2019-latest")
        .Build();

    private DbConnection dbConnection = default!;

    public async Task InitializeAsync()
    {
        await msSqlContainer.StartAsync();
        dbConnection = new SqlConnection(msSqlContainer.GetConnectionString());
        await seedDatabaseAsync();
    }

    public new async Task DisposeAsync()
    {
        await msSqlContainer.StopAsync();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            services.RemoveAll(typeof(DbContextOptions<DataContext>));
            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(dbConnection);
            });
        });
    }

    private async Task seedDatabaseAsync()
    {
        using var scope = Services.CreateScope();
        var dataContext = scope.ServiceProvider.GetRequiredService<IDataContext>();
        await dataContext.Database.MigrateAsync();
        await dataContext.Database.EnsureCreatedAsync();
        await dataContext.Seed();
    }
}
