using FluentValidation;

namespace SimpleDiplomBackend.Application.Features.Dishes.Commands.UpdateDish
{
    public class UpdateDishCategoryCommandValidator : AbstractValidator<UpdateDishCommand>
    {
        public UpdateDishCategoryCommandValidator()
        {
        }
    }
}