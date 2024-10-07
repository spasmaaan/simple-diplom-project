using Mediator;
using SimpleDiplomBackend.Application.Shared.Exceptions;
using SimpleDiplomBackend.Application.Shared.Interface;

namespace SimpleDiplomBackend.Application.Features.Review.Commands.DeleteReview
{
    public record DeleteReviewCommand : IRequest
    {
        public long Id { get; set; }
    }

    public class DeleteReviewCommandHandler : IRequestHandler<DeleteReviewCommand>
    {
        private readonly ISimpleDiplomBackendDbContext _dbContext;

        public DeleteReviewCommandHandler(ISimpleDiplomBackendDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async ValueTask<Unit> Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
        {
            var entity = _dbContext.Reviews.FirstOrDefault(p => p.Id == request.Id);

            if (entity == null)
            {
                throw new NotFoundException();
            }

            _dbContext.Reviews.Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;

        }
    }
}