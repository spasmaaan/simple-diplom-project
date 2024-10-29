using MediatR;
using Microsoft.EntityFrameworkCore;
using SimpleDiplomBackend.Application.Shared.Extensions;
using SimpleDiplomBackend.Application.Shared.Interface;
using SimpleDiplomBackend.Application.Shared.Models;

namespace SimpleDiplomBackend.Application.Features.Service.Queries.GetAll
{
    public record GetAllQuery : IRequest<PaginatedList<CommercialServiceDto>>
    {
        public int Offset { get; set; }
        public int Limit { get; set; }
    }

    public class GetAllQueryHandler : IRequestHandler<GetAllQuery, PaginatedList<CommercialServiceDto>>
    {
        private readonly ISimpleDiplomBackendDbContext _dbContext;

        public GetAllQueryHandler(ISimpleDiplomBackendDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PaginatedList<CommercialServiceDto>> Handle(GetAllQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.CommercialServices
                .AsNoTracking()
                .Select(p => new CommercialServiceDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,               
                })
                .OrderBy(p => p.Name)
                .PaginatedListAsync(request.Offset, request.Limit, cancellationToken);
        }
    }
}