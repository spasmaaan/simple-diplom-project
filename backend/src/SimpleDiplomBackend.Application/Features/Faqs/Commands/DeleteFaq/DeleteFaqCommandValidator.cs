using FluentValidation;

namespace SimpleDiplomBackend.Application.Features.Faqs.Commands.DeleteFaq
{
    public class DeleteFaqCommandValidator : AbstractValidator<DeleteFaqCommand>
    {
        public DeleteFaqCommandValidator()
        {
            RuleFor(v => v.Id)
                .NotEmpty().WithMessage("Id field is required.")
                .GreaterThan(0).WithMessage("Id value must be greater than zero.");
        }
    }
}