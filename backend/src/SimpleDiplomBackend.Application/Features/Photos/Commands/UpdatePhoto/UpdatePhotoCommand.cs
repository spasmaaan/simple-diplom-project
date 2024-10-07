using Mediator;
using SimpleDiplomBackend.Application.Features.Photos.Interfaces;
using SimpleDiplomBackend.Application.Shared.Exceptions;

namespace SimpleDiplomBackend.Application.Features.Photo.Commands.UpdatePhoto
{
    public record UpdatePhotoCommand : IRequest
    {
        public int Id { get; set; }
        public byte[]? Image { get; set; }
    }

    public class UpdatePhotoCommandHandler : IRequestHandler<UpdatePhotoCommand>
    {
        private readonly IPhotosRepository _photosReporsitory;

        public UpdatePhotoCommandHandler(IPhotosRepository dishRepository)
        {
            _photosReporsitory = dishRepository;
        }

        public async ValueTask<Unit> Handle(UpdatePhotoCommand request, CancellationToken cancellationToken)
        {

            var entity = await _photosReporsitory.GetById(request.Id);
            // check if dish was found.
            if (entity == null)
            {
                throw new NotFoundException(nameof(Dishes), request.Id);
            }

            if (request.Image != null)
            {
                entity.Image = request.Image;
            }

            // update dish record
            await _photosReporsitory.Update(entity);

            return Unit.Value;

        }
    }
}