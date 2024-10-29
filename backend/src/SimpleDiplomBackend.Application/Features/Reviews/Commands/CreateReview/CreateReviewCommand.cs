using MediatR;
using SimpleDiplomBackend.Application.Shared.Interface;

namespace SimpleDiplomBackend.Application.Features.Review.Commands.CreateReview
{
    public record CreateReviewCommand : IRequest<Domain.Entities.Review>
    {
        public string AccessToken { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public short? Rating { get; set; }
    }

    public class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand, Domain.Entities.Review>
    {
        private readonly ISimpleDiplomBackendDbContext _dbContext;

        public CreateReviewCommandHandler(ISimpleDiplomBackendDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Domain.Entities.Review> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
        {

            var entity = new Domain.Entities.Review
            {
                UserId = "current user id",
                Message = request.Message,
                Rating = request.Rating,
                CreationDate = DateTime.Now
            };

            var added = _dbContext.Reviews.Add(entity);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return added.Entity;
        }
    }
}