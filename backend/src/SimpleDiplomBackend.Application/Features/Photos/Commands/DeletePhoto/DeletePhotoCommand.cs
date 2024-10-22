using MediatR;
using SimpleDiplomBackend.Application.Shared.Exceptions;
using SimpleDiplomBackend.Application.Shared.Interface;

namespace SimpleDiplomBackend.Application.Features.Photo.Commands.DeletePhoto
{
    public record DeletePhotoCommand : IRequest<Domain.Entities.Photo>
    {
        public int Id { get; set; }
    }

    public class DeletePhotoCommandHandler : IRequestHandler<DeletePhotoCommand, Domain.Entities.Photo>
    {
        private readonly ISimpleDiplomBackendDbContext _dbContext;

        public DeletePhotoCommandHandler(ISimpleDiplomBackendDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Domain.Entities.Photo> Handle(DeletePhotoCommand request, CancellationToken cancellationToken)
        {
            var entity = _dbContext.Photos.FirstOrDefault(p => p.Id == request.Id);

            if (entity == null)
            {
                throw new NotFoundException();
            }

            _dbContext.Photos.Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return entity;
        }
    }
}