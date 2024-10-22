using MediatR;
using SimpleDiplomBackend.Application.Features.Photos.Interfaces;
using SimpleDiplomBackend.Application.Shared.Exceptions;

namespace SimpleDiplomBackend.Application.Features.Photo.Commands.UpdatePhoto
{
    public record UpdatePhotoCommand : IRequest<Domain.Entities.Photo>
    {
        public int Id { get; set; }
        public byte[]? Image { get; set; }
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

            if (request.Image != null)
            {
                entity.Image = request.Image;
            }

            await _photosReporsitory.Update(entity);

            return entity;
        }
    }
}