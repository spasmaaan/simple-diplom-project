using FluentValidation;
using SimpleDiplomBackend.Booking.Features.Photo.Commands.CreatePhoto;

namespace SimpleDiplomBackend.Application.Features.Photo.Commands.CreatePhoto
{
    public class CreatePhotoCommandValidator : AbstractValidator<CreatePhotoCommand>
    {
        public CreatePhotoCommandValidator()
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