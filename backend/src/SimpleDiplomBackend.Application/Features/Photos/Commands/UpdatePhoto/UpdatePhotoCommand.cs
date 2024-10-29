using MediatR;
using SimpleDiplomBackend.Application.Features.Photos.Interfaces;
using SimpleDiplomBackend.Application.Shared.Exceptions;
using SimpleDiplomBackend.Application.Shared.Utilities;

namespace SimpleDiplomBackend.Application.Features.Photo.Commands.UpdatePhoto
{
    public record UpdatePhotoCommand : IRequest<Domain.Entities.Photo>
    {
        public int Id { get; set; }
        public string ImageBase64 { get; set; } = string.Empty;
    }

    public class UpdatePhotoCommandHandler : IRequestHandler<UpdatePhotoCommand, Domain.Entities.Photo>
    {
        private readonly IPhotosRepository _photosReporsitory;

        public UpdatePhotoCommandHandler(IPhotosRepository dishRepository)
        {
            _photosReporsitory = dishRepository;
        }

        public async Task<Domain.Entities.Photo> Handle(UpdatePhotoCommand request, CancellationToken cancellationToken)
        {
            var entity = await _photosReporsitory.GetById(request.Id);
            if (entity == null)
            {
                throw new NotFoundException(nameof(Dishes), request.Id);
            }

            var image = new Base64FileInfo(request.ImageBase64);
            if (request.ImageBase64 != null)
            {

                entity.MimeType = image.MimeType;
                entity.Image = image.Data;
            }

            await _photosReporsitory.Update(entity);

            return entity;
        }
    }
}