using MediatR;
using SimpleDiplomBackend.Application.Features.Dishes.Interfaces;
using SimpleDiplomBackend.Application.Shared.Exceptions;
using SimpleDiplomBackend.Domain.Entities;

namespace SimpleDiplomBackend.Application.Features.Dishes.Commands.UpdateDish
{
    public record UpdateDishCategoryCommand : IRequest<DishCategory>
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public byte[]? PreviewImage { get; set; }
    }

    public class UpdateDishCategoryCommandHandler : IRequestHandler<UpdateDishCategoryCommand, DishCategory>
    {
        private readonly IDishRepository _dishReporsitory;

        public UpdateDishCategoryCommandHandler(IDishRepository dishRepository)
        {
            _dishReporsitory = dishRepository;
        }

        public async Task<DishCategory> Handle(UpdateDishCategoryCommand request, CancellationToken cancellationToken)
        {

            var entity = await _dishReporsitory.GetCategoryById(request.Id);
            // check if dish was found.
            if (entity == null)
            {
                throw new NotFoundException($"Category of {nameof(Dishes)}", request.Id);
            }

            if (request.Name != null)
            {
                entity.Name = request.Name;
            }
            if (request.Description != null)
            {
                entity.Description = request.Description;
            }
            if (request.PreviewImage != null)
            {
                entity.PreviewImage = request.PreviewImage;
            }

            // update dish record
            await _dishReporsitory.UpdateCategory(entity);

            return entity;
        }
    }
}