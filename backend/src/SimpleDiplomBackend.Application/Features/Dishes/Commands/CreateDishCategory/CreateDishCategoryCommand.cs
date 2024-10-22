using MediatR;
using SimpleDiplomBackend.Application.Shared.Interface;
using SimpleDiplomBackend.Domain.Entities;

namespace SimpleDiplomBackend.Application.Features.Dishes.Commands
{
    public record CreateDishCategoryCommand : IRequest<DishCategory>
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public byte[]? PreviewImage { get; set; }
    }

    public class CreateDishCategoryCommandHandler : IRequestHandler<CreateDishCategoryCommand, DishCategory>
    {
        private readonly ISimpleDiplomBackendDbContext _dbContext;

        public CreateDishCategoryCommandHandler(ISimpleDiplomBackendDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<DishCategory> Handle(CreateDishCategoryCommand request, CancellationToken cancellationToken)
        {

            var entity = new DishCategory
            {
                Name = request.Name,
                Description = request.Description,
                PreviewImage = request.PreviewImage,
            };

            var added = _dbContext.DishCategories.Add(entity);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return added.Entity;
        }
    }

}