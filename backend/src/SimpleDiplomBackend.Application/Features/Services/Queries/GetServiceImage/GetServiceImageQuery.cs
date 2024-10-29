using MediatR;
using Microsoft.EntityFrameworkCore;
using SimpleDiplomBackend.Application.Shared.Interface;
using SimpleDiplomBackend.Domain.Entities;

namespace SimpleDiplomBackend.Application.Features.Services.Queries.GetServiceImage
{
    public record GetServiceImageQuery : IRequest<GetServiceImage?>
    {
        public int Id { get; set; }
    }

    public class GetServiceImageQueryHandler : IRequestHandler<GetServiceImageQuery, GetServiceImage?>
    {
        private readonly ISimpleDiplomBackendDbContext _dbContext;

        public GetServiceImageQueryHandler(ISimpleDiplomBackendDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<GetServiceImage?> Handle(GetServiceImageQuery request, CancellationToken cancellationToken)
        {
            CommercialService? service = await _dbContext.CommercialServices
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == request.Id);
            return service != null 
                ? new GetServiceImage
                { 
                    Data = service.PreviewImage ?? Array.Empty<byte>(),
                    MimeType = service.PreviewMimeType,
                }
                : null;
        }
    }
}