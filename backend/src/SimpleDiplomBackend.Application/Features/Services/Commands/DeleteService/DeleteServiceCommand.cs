using MediatR;
using SimpleDiplomBackend.Application.Shared.Exceptions;
using SimpleDiplomBackend.Application.Shared.Interface;
using SimpleDiplomBackend.Domain.Entities;

namespace SimpleDiplomBackend.Application.Features.Service.Commands.DeleteService
{
    public record DeleteServiceCommand : IRequest<CommercialService>
    {
        public long Id { get; set; }
    }

    public class DeleteServiceCommandHandler : IRequestHandler<DeleteServiceCommand, CommercialService>
    {
        private readonly ISimpleDiplomBackendDbContext _dbContext;

        public DeleteServiceCommandHandler(ISimpleDiplomBackendDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CommercialService> Handle(DeleteServiceCommand request, CancellationToken cancellationToken)
        {
            var entity = _dbContext.CommercialServices.FirstOrDefault(p => p.Id == request.Id);

            if (entity == null)
            {
                throw new NotFoundException();
            }

            var removed = _dbContext.CommercialServices.Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return removed.Entity;
        }
    }
}