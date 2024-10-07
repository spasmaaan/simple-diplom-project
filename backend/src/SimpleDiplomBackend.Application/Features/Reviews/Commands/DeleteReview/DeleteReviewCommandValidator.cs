using FluentValidation;

namespace SimpleDiplomBackend.Application.Features.Review.Commands.DeleteReview
{
    public class DeleteReviewCommandValidator : AbstractValidator<DeleteReviewCommand>
    {
        public DeleteReviewCommandValidator()
        {
            RuleFor(v => v.Id)
                .NotEmpty().WithMessage("Id field is required.")
                .GreaterThan(0).WithMessage("Id value must be greater than zero.");
        }
    }
}