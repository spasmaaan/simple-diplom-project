using FluentValidation;

namespace SimpleDiplomBackend.Application.Features.Dishes.Commands.CreateDish
{
    public class CreateDishCategoryCommandValidator : AbstractValidator<CreateDishCategoryCommand>
    {
        public CreateDishCategoryCommandValidator()
        {
            RuleFor(v => v.Name)
                .NotEmpty().WithMessage("Dish name field is required!")
                .MaximumLength(100).WithMessage("Dish name has a maximum field size of 100 characters.");

            RuleFor(v => v.Description)
                .NotEmpty().WithMessage("Dish description field is required!")
                .MaximumLength(100).WithMessage("Dish description has a maximum field size of 500 characters.");
        }
    }
}