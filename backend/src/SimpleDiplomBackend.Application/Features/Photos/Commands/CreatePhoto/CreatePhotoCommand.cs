using Mediator;
using SimpleDiplomBackend.Application.Shared.Interface;

namespace SimpleDiplomBackend.Booking.Features.Photo.Commands.CreatePhoto
{
    public record CreatePhotoCommand : IRequest
    {
        public byte[]? Image { get; set; }
    }

    public class CreatePhotoCommandHandler : IRequestHandler<CreatePhotoCommand>
    {
        private readonly ISimpleDiplomBackendDbContext _dbContext;

        public CreatePhotoCommandHandler(ISimpleDiplomBackendDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async ValueTask<Unit> Handle(CreatePhotoCommand request, CancellationToken cancellationToken)
        {

            var entity = new Domain.Entities.Photo
            {
                Image = request.Image
            };

            _dbContext.Photos.Add(entity);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }

}