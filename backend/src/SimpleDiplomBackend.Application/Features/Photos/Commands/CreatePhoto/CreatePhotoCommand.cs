using MediatR;
using SimpleDiplomBackend.Application.Shared.Interface;
using SimpleDiplomBackend.Application.Shared.Utilities;
using SimpleDiplomBackend.Domain.Entities;

namespace SimpleDiplomBackend.Booking.Features.Photo.Commands.CreatePhoto
{
    public record CreatePhotoCommand : IRequest<Domain.Entities.Photo>
    {
        public string ImageBase64 { get; set; } = string.Empty;
    }

    public class CreatePhotoCommandHandler : IRequestHandler<CreatePhotoCommand, Domain.Entities.Photo>
    {
        private readonly ISimpleDiplomBackendDbContext _dbContext;

        public CreatePhotoCommandHandler(ISimpleDiplomBackendDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Domain.Entities.Photo> Handle(CreatePhotoCommand request, CancellationToken cancellationToken)
        {
            var image = new Base64FileInfo(request.ImageBase64);
            var entity = new Domain.Entities.Photo
            {
                MimeType = image.MimeType,
                Image = image.Data,
            };

            _dbContext.Photos.Add(entity);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return entity;
        }
    }

}