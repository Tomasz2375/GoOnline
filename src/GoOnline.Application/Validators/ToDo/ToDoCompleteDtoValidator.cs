using FluentValidation;
using GoOnline.Application.Dtos.ToDo;

namespace GoOnline.Application.Validators.ToDo;

public class ToDoCompleteDtoValidator : AbstractValidator<ToDoCompleteDto>
{
    public ToDoCompleteDtoValidator()
    {
        RuleFor(x => x.Complete).NotNull().InclusiveBetween(0, 100);
    }
}
