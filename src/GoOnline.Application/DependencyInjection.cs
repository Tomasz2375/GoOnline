using FluentValidation;
using GoOnline.Application.Dtos.ToDo;
using GoOnline.Application.Validators.ToDo;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace GoOnline.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        services.AddMediatR(x => x.RegisterServicesFromAssemblies(assembly));
        services.AddScoped<IMapper, Mapper>();
        services.AddScoped<IValidator<ToDoCompleteDto>, ToDoCompleteDtoValidator>();
        services.AddScoped<IValidator<ToDoListDto>, ToDoListDtoValidator>();
        services.AddScoped<IValidator<ToDoDetailsDto>, ToDoDetailsDtoValidator>();

        return services;
    }
}
