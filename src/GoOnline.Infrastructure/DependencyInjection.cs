using GoOnline.Domain.Interfaces;
using GoOnline.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GoOnline.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DataContext>(options => options.UseSqlServer(
            configuration.GetConnectionString("GoOnlineDbConnection")));
        services.AddTransient<IDataContext, DataContext>();

        return services;
    }
}
