using FluentValidation;

namespace SimpleDiplomBackend.Application.Features.Service.Commands.DeleteService
{
    public class DeleteServiceCommandValidator : AbstractValidator<DeleteServiceCommand>
    {
        public DeleteServiceCommandValidator()
        {
            RuleFor(v => v.Id)
                .NotEmpty().WithMessage("Id field is required.")
                .GreaterThan(0).WithMessage("Id value must be greater than zero.");
        }
    }
}