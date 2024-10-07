using Mediator;
using SimpleDiplomBackend.Application.Shared.Interface;
using SimpleDiplomBackend.Domain.Entities;

namespace SimpleDiplomBackend.Application.Features.Dishes.Commands
{
    public record CreateDishCategoryCommand : IRequest
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public byte[]? PreviewImage { get; set; }
    }

    public class CreateDishCategoryCommandHandler : IRequestHandler<CreateDishCategoryCommand>
    {
        private readonly ISimpleDiplomBackendDbContext _dbContext;

        public CreateDishCategoryCommandHandler(ISimpleDiplomBackendDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async ValueTask<Unit> Handle(CreateDishCategoryCommand request, CancellationToken cancellationToken)
        {

            var entity = new DishCategory
            {
                Name = request.Name,
                Description = request.Description,
                PreviewImage = request.PreviewImage,
            };

            _dbContext.DishCategories.Add(entity);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }

}