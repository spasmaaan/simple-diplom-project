using FluentValidation;

namespace SimpleDiplomBackend.Application.Features.Photo.Commands.DeletePhoto
{
    public class DeletePhotoCommandValidator : AbstractValidator<DeletePhotoCommand>
    {
        public DeletePhotoCommandValidator()
        {
            RuleFor(v => v.Id)
                .NotEmpty().WithMessage("Id field is required.")
                .GreaterThan(0).WithMessage("Id value must be greater than zero.");
        }
    }
}