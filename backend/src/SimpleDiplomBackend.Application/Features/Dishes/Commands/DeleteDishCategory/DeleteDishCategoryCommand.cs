using Mediator;
using SimpleDiplomBackend.Application.Shared.Exceptions;
using SimpleDiplomBackend.Application.Shared.Interface;

namespace SimpleDiplomBackend.Application.Features.Dishes.Commands.DeleteDish
{
    public record DeleteDishCategoryCommand : IRequest
    {
        public long Id { get; set; }
    }

    public class DeleteDishCategoryCommandHandler : IRequestHandler<DeleteDishCategoryCommand>
    {
        private readonly ISimpleDiplomBackendDbContext _dbContext;

        public DeleteDishCategoryCommandHandler(ISimpleDiplomBackendDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async ValueTask<Unit> Handle(DeleteDishCategoryCommand request, CancellationToken cancellationToken)
        {
            var entity = _dbContext.DishCategories.FirstOrDefault(p => p.Id == request.Id);

            if (entity == null)
            {
                throw new NotFoundException();
            }

            _dbContext.DishCategories.Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;

        }
    }
}