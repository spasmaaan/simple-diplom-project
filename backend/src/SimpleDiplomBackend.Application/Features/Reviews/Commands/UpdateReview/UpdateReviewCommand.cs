using MediatR;
using SimpleDiplomBackend.Application.Features.Photos.Interfaces;
using SimpleDiplomBackend.Application.Shared.Exceptions;

namespace SimpleDiplomBackend.Application.Features.Review.Commands.UpdateReview
{
    public record UpdateReviewCommand : IRequest<Domain.Entities.Review>
    {
        public int Id { get; set; }
        public string Message { get; set; } = string.Empty;
        public short? Rating { get; set; }
    }

    public class UpdateReviewCommandHandler : IRequestHandler<UpdateReviewCommand, Domain.Entities.Review>
    {
        private readonly IReviewsRepository _reviewsReporsitory;

        public UpdateReviewCommandHandler(IReviewsRepository dishRepository)
        {
            _reviewsReporsitory = dishRepository;
        }

        public async Task<Domain.Entities.Review> Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
        {
            var entity = await _reviewsReporsitory.GetById(request.Id);
            // check if dish was found.
            if (entity == null)
            {
                throw new NotFoundException(nameof(Review), request.Id);
            }

            if (request.Message != null)
            {
                entity.Message = request.Message;
            }
            if (request.Rating != null)
            {
                entity.Rating = request.Rating;
            }
            // update dish record
            await _reviewsReporsitory.Update(entity);

            return entity;
        }
    }
}