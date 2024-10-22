using MediatR;
using SimpleDiplomBackend.Application.Shared.Exceptions;
using SimpleDiplomBackend.Application.Shared.Interface;
using SimpleDiplomBackend.Domain.Entities;

namespace SimpleDiplomBackend.Application.Features.Faqs.Commands.DeleteFaq
{
    public record DeleteFaqCommand : IRequest<Faq>
    {
        public int Id { get; set; }
    }

    public class DeleteFaqCommandHandler : IRequestHandler<DeleteFaqCommand, Faq>
    {
        private readonly ISimpleDiplomBackendDbContext _dbContext;

        public DeleteFaqCommandHandler(ISimpleDiplomBackendDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Faq> Handle(DeleteFaqCommand request, CancellationToken cancellationToken)
        {
            var entity = _dbContext.Faqs.FirstOrDefault(p => p.Id == request.Id);

            if (entity == null)
            {
                throw new NotFoundException();
            }

            var removed = _dbContext.Faqs.Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return removed.Entity;
        }
    }
}