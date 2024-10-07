using FluentValidation;

namespace SimpleDiplomBackend.Application.Features.Booking.Commands.DeleteBooking
{
    public class DeleteBookingCommandValidator : AbstractValidator<DeleteBookingCommand>
    {
        public DeleteBookingCommandValidator()
        {
            RuleFor(v => v.Id)
                .NotEmpty().WithMessage("Id field is required.")
                .GreaterThan(0).WithMessage("Id value must be greater than zero.");
        }
    }
}