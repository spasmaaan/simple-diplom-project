using FluentValidation;

namespace SimpleDiplomBackend.Application.Features.Photo.Commands.UpdatePhoto
{
    public class UpdatePhotoCommandValidator : AbstractValidator<UpdatePhotoCommand>
    {
        public UpdatePhotoCommandValidator()
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