using Mediator;
using SimpleDiplomBackend.Application.Shared.Interface;
using SimpleDiplomBackend.Domain.Entities;

namespace SimpleDiplomBackend.Booking.Features.Review.Commands.CreateReview
{
    public record CreateReviewCommand : IRequest
    {
        public string Message { get; set; } = string.Empty;
        public short? Rating { get; set; }
    }

    public class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand>
    {
        private readonly ISimpleDiplomBackendDbContext _dbContext;

        public CreateReviewCommandHandler(ISimpleDiplomBackendDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async ValueTask<Unit> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
        {

            var entity = new Domain.Entities.Review
            {
                UserId = "current user id",
                Message = request.Message,
                Rating = request.Rating,
                CreationDate = DateTime.Now
            };

            _dbContext.Reviews.Add(entity);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }

}