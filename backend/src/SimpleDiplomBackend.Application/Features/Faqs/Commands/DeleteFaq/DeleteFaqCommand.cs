using Mediator;
using SimpleDiplomBackend.Application.Shared.Exceptions;
using SimpleDiplomBackend.Application.Shared.Interface;

namespace SimpleDiplomBackend.Application.Features.Faqs.Commands.DeleteFaq
{
    public record DeleteFaqCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteFaqCommandHandler : IRequestHandler<DeleteFaqCommand>
    {
        private readonly ISimpleDiplomBackendDbContext _dbContext;

        public DeleteFaqCommandHandler(ISimpleDiplomBackendDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async ValueTask<Unit> Handle(DeleteFaqCommand request, CancellationToken cancellationToken)
        {
            var entity = _dbContext.Faqs.FirstOrDefault(p => p.Id == request.Id);

            if (entity == null)
            {
                throw new NotFoundException();
            }

            _dbContext.Faqs.Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;

        }
    }
}