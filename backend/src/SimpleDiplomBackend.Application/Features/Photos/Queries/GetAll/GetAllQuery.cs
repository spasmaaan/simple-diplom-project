using MediatR;
using Microsoft.EntityFrameworkCore;
using SimpleDiplomBackend.Application.Shared.Extensions;
using SimpleDiplomBackend.Application.Shared.Interface;
using SimpleDiplomBackend.Application.Shared.Models;

namespace SimpleDiplomBackend.Application.Features.Photo.Queries.GetAll
{
    public record GetAllQuery : IRequest<PaginatedList<PhotoDto>>
    {
        public int Offset { get; set; }
        public int Limit { get; set; }
    }

    public class GetAllQueryHandler : IRequestHandler<GetAllQuery, PaginatedList<PhotoDto>>
    {
        private readonly ISimpleDiplomBackendDbContext _dbContext;

        public GetAllQueryHandler(ISimpleDiplomBackendDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PaginatedList<PhotoDto>> Handle(GetAllQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.Photos
                .AsNoTracking()
                .Select(p => new PhotoDto
                {
                    Id = p.Id,
                    Image = p.Image               
                })
                .OrderByDescending(p => p.Id)
                .PaginatedListAsync(request.Offset, request.Limit, cancellationToken);
        }
    }
}