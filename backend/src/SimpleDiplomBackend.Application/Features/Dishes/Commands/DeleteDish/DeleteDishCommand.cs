using MediatR;
using SimpleDiplomBackend.Application.Shared.Exceptions;
using SimpleDiplomBackend.Application.Shared.Interface;
using SimpleDiplomBackend.Domain.Entities;

namespace SimpleDiplomBackend.Application.Features.Dishes.Commands.DeleteDish
{
    public record DeleteDishCommand : IRequest<Dish>
    {
        public long Id { get; set; }
    }

    public class DeleteDishCommandHandler : IRequestHandler<DeleteDishCommand, Dish>
    {
        private readonly ISimpleDiplomBackendDbContext _dbContext;

        public DeleteDishCommandHandler(ISimpleDiplomBackendDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Dish> Handle(DeleteDishCommand request, CancellationToken cancellationToken)
        {
            var entity = _dbContext.Dishes.FirstOrDefault(p => p.Id == request.Id);

            if (entity == null)
            {
                throw new NotFoundException();
            }

            var removed = _dbContext.Dishes.Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return removed.Entity;
        }
    }
}