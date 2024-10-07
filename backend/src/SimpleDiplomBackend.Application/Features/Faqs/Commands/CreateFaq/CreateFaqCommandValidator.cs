using FluentValidation;
using SimpleDiplomBackend.Booking.Features.Faqs.Commands.CreateFaq;

namespace SimpleDiplomBackend.Application.Features.Faqs.Commands.CreateFaq
{
    public class CreateFaqCommandValidator : AbstractValidator<CreateFaqCommand>
    {
        public CreateFaqCommandValidator()
        {
            RuleFor(v => v.Question)
                .NotEmpty().WithMessage("Dish name field is required!")
                .MaximumLength(100).WithMessage("Dish name has a maximum field size of 100 characters.");

            RuleFor(v => v.Answer)
                .NotEmpty().WithMessage("Dish description field is required!")
                .MaximumLength(100).WithMessage("Dish description has a maximum field size of 500 characters.");
        }
    }
}