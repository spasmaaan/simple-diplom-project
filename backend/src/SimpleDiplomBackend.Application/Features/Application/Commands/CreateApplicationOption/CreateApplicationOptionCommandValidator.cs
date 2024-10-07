using FluentValidation;
using SimpleDiplomBackend.Application.Features.Dishes.Commands;

namespace SimpleDiplomBackend.Application.Features.Application.Commands.CreateApplicationOption
{
    public class CreateApplicationOptionCommandValidator : AbstractValidator<CreateApplicationOptionCommand>
    {
        public CreateApplicationOptionCommandValidator()
        {
            RuleFor(v => v.Name)
                .NotEmpty().WithMessage("Dish name field is required!")
                .MaximumLength(100).WithMessage("Dish name has a maximum field size of 100 characters.");

            RuleFor(v => v.Description)
                .NotEmpty().WithMessage("Dish description field is required!")
                .MaximumLength(100).WithMessage("Dish description has a maximum field size of 500 characters.");

            RuleFor(v => v.Price)
                .NotEmpty().WithMessage("Dish price field is required!")
                .GreaterThan(0).WithMessage("");
        }
    }
}