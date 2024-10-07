using Mediator;
using SimpleDiplomBackend.Application.Shared.Exceptions;
using SimpleDiplomBackend.Application.Shared.Interface;

namespace SimpleDiplomBackend.Application.Features.Service.Commands.DeleteService
{
    public record DeleteServiceCommand : IRequest
    {
        public long Id { get; set; }
    }

    public class DeleteServiceCommandHandler : IRequestHandler<DeleteServiceCommand>
    {
        private readonly ISimpleDiplomBackendDbContext _dbContext;

        public DeleteServiceCommandHandler(ISimpleDiplomBackendDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async ValueTask<Unit> Handle(DeleteServiceCommand request, CancellationToken cancellationToken)
        {
            var entity = _dbContext.CommercialServices.FirstOrDefault(p => p.Id == request.Id);

            if (entity == null)
            {
                throw new NotFoundException();
            }

            _dbContext.CommercialServices.Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;

        }
    }
}