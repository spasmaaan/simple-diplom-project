using FluentValidation;

namespace SimpleDiplomBackend.Application.Features.Dishes.Commands.DeleteDish
{
    public class DeleteDishCommandValidator : AbstractValidator<DeleteDishCommand>
    {
        public DeleteDishCommandValidator()
        {
            RuleFor(v => v.Id)
                .NotEmpty().WithMessage("Id field is required.")
                .GreaterThan(0).WithMessage("Id value must be greater than zero.");
        }
    }
}