using FluentValidation;

namespace SimpleDiplomBackend.Application.Features.Faqs.Commands.UpdateFaq
{
    public class UpdateFaqCommandValidator : AbstractValidator<UpdateFaqCommand>
    {
        public UpdateFaqCommandValidator()
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