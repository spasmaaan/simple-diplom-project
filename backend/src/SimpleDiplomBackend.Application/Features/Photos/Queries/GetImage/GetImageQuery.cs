using MediatR;
using Microsoft.EntityFrameworkCore;
using SimpleDiplomBackend.Application.Shared.Interface;

namespace SimpleDiplomBackend.Application.Features.Photos.Queries.GetImage
{
    public record GetImageQuery : IRequest<PhotoImageDto?>
    {
        public int Id { get; set; }
    }

    public class GetImageQueryHandler : IRequestHandler<GetImageQuery, PhotoImageDto?>
    {
        private readonly ISimpleDiplomBackendDbContext _dbContext;

        public GetImageQueryHandler(ISimpleDiplomBackendDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PhotoImageDto?> Handle(GetImageQuery request, CancellationToken cancellationToken)
        {
            Domain.Entities.Photo? photo = await _dbContext.Photos
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == request.Id);
            return photo != null 
                ? new PhotoImageDto { 
                    Data = photo.Image ?? Array.Empty<byte>(),
                    MimeType = photo.MimeType,
                }
                : null;
        }
    }
}