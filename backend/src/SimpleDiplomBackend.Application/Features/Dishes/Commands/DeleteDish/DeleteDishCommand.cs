﻿using Mediator;
using SimpleDiplomBackend.Application.Shared.Exceptions;
using SimpleDiplomBackend.Application.Shared.Interface;

namespace SimpleDiplomBackend.Application.Features.Dishes.Commands.DeleteDish
{
    public record DeleteDishCommand : IRequest
    {
        public long Id { get; set; }
    }

    public class DeleteDishCommandHandler : IRequestHandler<DeleteDishCommand>
    {
        private readonly ISimpleDiplomBackendDbContext _dbContext;

        public DeleteDishCommandHandler(ISimpleDiplomBackendDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async ValueTask<Unit> Handle(DeleteDishCommand request, CancellationToken cancellationToken)
        {
            var entity = _dbContext.Dishes.FirstOrDefault(p => p.Id == request.Id);

            if (entity == null)
            {
                throw new NotFoundException();
            }

            _dbContext.Dishes.Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;

        }
    }
}