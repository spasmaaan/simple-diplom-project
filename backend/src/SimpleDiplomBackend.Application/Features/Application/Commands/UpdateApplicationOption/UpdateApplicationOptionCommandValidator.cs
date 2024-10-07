using FluentValidation;

namespace SimpleDiplomBackend.Application.Features.Application.Commands.UpdateApplicationOption
{
    public class UpdateApplicationOptionCommandValidator : AbstractValidator<UpdateApplicationOptionCommand>
    {
        public UpdateApplicationOptionCommandValidator()
        {
            RuleFor(v => v.Id)
                .GreaterThan(0).WithMessage("Dish Id value is required.");

            RuleFor(v => v.Name)
                .NotEmpty().WithMessage("Dish Name field is required.");

            RuleFor(v => v.Description)
                .NotEmpty().WithMessage("Dish Name field is required.");

            RuleFor(v => v.Price)
                .NotEmpty().WithMessage("Dish Name field is required.");
        }
    }
}