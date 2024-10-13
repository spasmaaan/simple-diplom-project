using FluentValidation;
using SimpleDiplomBackend.Booking.Features.Photo.Commands.CreatePhoto;

namespace SimpleDiplomBackend.Application.Features.Photo.Commands.CreatePhoto
{
    public class CreatePhotoCommandValidator : AbstractValidator<CreatePhotoCommand>
    {
        public CreatePhotoCommandValidator()
        {
        }
    }
}