using FluentValidation;

namespace SimpleDiplomBackend.Application.Features.Application.Commands.DeleteApplicationOption
{
    public class DeleteApplicationOptionCommandValidator : AbstractValidator<DeleteApplicationOptionCommand>
    {
        public DeleteApplicationOptionCommandValidator()
        {
            RuleFor(v => v.Id)
                .NotEmpty().WithMessage("Id field is required.")
                .GreaterThan(0).WithMessage("Id value must be greater than zero.");
        }
    }
}