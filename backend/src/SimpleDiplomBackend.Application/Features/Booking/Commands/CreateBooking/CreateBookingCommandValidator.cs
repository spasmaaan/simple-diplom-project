using FluentValidation;

namespace SimpleDiplomBackend.Application.Features.Booking.Commands.CreateBooking
{
    public class CreateBookingCommandValidator : AbstractValidator<CreateBookingCommand>
    {
        public CreateBookingCommandValidator()
        {
        }
    }
}