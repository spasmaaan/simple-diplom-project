using Mediator;
using SimpleDiplomBackend.Application.Features.Dishes.Interfaces;
using SimpleDiplomBackend.Application.Shared.Exceptions;

namespace SimpleDiplomBackend.Application.Features.Dishes.Commands.UpdateDish
{
    public record UpdateDishCommand : IRequest
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public byte[]? PreviewImage { get; set; }
        public decimal? Price { get; set; }
        public int? CategoryId { get; set; }
    }

    public class UpdateProductCommandHandler : IRequestHandler<UpdateDishCommand>
    {
        private readonly IDishRepository _dishReporsitory;

        public UpdateProductCommandHandler(IDishRepository dishRepository)
        {
            _dishReporsitory = dishRepository;
        }

        public async ValueTask<Unit> Handle(UpdateDishCommand request, CancellationToken cancellationToken)
        {

            var entity = await _dishReporsitory.GetById(request.Id);
            // check if dish was found.
            if (entity == null)
            {
                throw new NotFoundException(nameof(Dishes), request.Id);
            }

            if (request.Name != null)
            {
                entity.Name = request.Name;
            }
            if (request.Description != null)
            {
                entity.Description = request.Description;
            }
            if (request.PreviewImage != null) {
                entity.PreviewImage = request.PreviewImage;
            }
            if (request.Price.HasValue)
            {
                entity.Price = request.Price.Value;
            }
            if (request.CategoryId.HasValue)
            {
                entity.CategoryId = request.CategoryId.Value;
            }

            // update dish record
            await _dishReporsitory.Update(entity);

            return Unit.Value;

        }
    }
}