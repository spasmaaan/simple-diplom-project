using FluentValidation;
using SimpleDiplomBackend.Application.Features.Dishes.Commands;

namespace SimpleDiplomBackend.Application.Features.Application.Commands.CreateApplicationOption
{
    public class CreateApplicationOptionCommandValidator : AbstractValidator<CreateApplicationOptionCommand>
    {
        public CreateApplicationOptionCommandValidator()
        {
        }
    }
}