﻿using FluentValidation;
using GoOnline.Application.Dtos.ToDo;

namespace GoOnline.Application.Validators.ToDo;

public class ToDoListDtoValidator : AbstractValidator<ToDoListDto>
{
    public ToDoListDtoValidator()
    {
        Include(new ToDoCompleteDtoValidator());

        RuleFor(x => x.Title).NotEmpty().MaximumLength(100);
    }
}
