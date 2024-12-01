using FluentValidation;
using GoOnline.Application.Dtos.ToDo;
using GoOnline.Application.Validators.ToDo;
using Microsoft.Extensions.DependencyInjection;

namespace GoOnline.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IValidator<ToDoCompleteDto>, ToDoCompleteDtoValidator>();
        services.AddScoped<IValidator<ToDoListDto>, ToDoListDtoValidator>();
        services.AddScoped<IValidator<ToDoDetailsDto>, ToDoDetailsDtoValidator>();

        return services;
    }
}
