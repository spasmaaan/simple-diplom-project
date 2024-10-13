using FluentValidation;

namespace SimpleDiplomBackend.Application.Features.Dishes.Commands.UpdateDish
{
    public class UpdateDishCommandValidator : AbstractValidator<UpdateDishCommand>
    {
        public UpdateDishCommandValidator()
        {
        }
    }
}