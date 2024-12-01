using FluentValidation;
using GoOnline.Application.Dtos.ToDo;

namespace GoOnline.Application.Validators.ToDo;

public class ToDoDetailsDtoValidator : AbstractValidator<ToDoDetailsDto>
{
    public ToDoDetailsDtoValidator()
    {
        Include(new ToDoCompleteDtoValidator());

        RuleFor(x => x.Title).NotEmpty().MaximumLength(100);
    }
}
