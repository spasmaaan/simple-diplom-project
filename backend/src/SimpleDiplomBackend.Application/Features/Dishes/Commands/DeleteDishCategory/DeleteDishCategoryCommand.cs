using MediatR;
using SimpleDiplomBackend.Application.Shared.Exceptions;
using SimpleDiplomBackend.Application.Shared.Interface;
using SimpleDiplomBackend.Domain.Entities;

namespace SimpleDiplomBackend.Application.Features.Dishes.Commands.DeleteDish
{
    public record DeleteDishCategoryCommand : IRequest<DishCategory>
    {
        public long Id { get; set; }
    }

    public class DeleteDishCategoryCommandHandler : IRequestHandler<DeleteDishCategoryCommand, DishCategory>
    {
        private readonly ISimpleDiplomBackendDbContext _dbContext;

        public DeleteDishCategoryCommandHandler(ISimpleDiplomBackendDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<DishCategory> Handle(DeleteDishCategoryCommand request, CancellationToken cancellationToken)
        {
            var entity = _dbContext.DishCategories.FirstOrDefault(p => p.Id == request.Id);

            if (entity == null)
            {
                throw new NotFoundException();
            }

            var remove = _dbContext.DishCategories.Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return remove.Entity;
        }
    }
}