﻿using FluentValidation;

namespace __PROJECT_NAME__.Application.Features.__FEATURE_NAME__.Commands.__COMMAND_NAME__
{
    public class __COMMAND_NAME__CommandValidator : AbstractValidator<__COMMAND_NAME__Command>
    {
        public __COMMAND_NAME__CommandValidator()
        {
            RuleFor(v => v.Property1).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Property1 field is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(v => v.Property2).Cascade(CascadeMode.Continue)
                .NotEmpty().WithMessage("Property2 field is required.")
                .MaximumLength(50).WithMessage("Property2 is over the maximum field length of 50.");

            RuleFor(v => v.Property3).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Property3 field is required.");
        }
    }
}