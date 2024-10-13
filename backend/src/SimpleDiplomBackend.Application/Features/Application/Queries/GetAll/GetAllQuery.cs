using Mediator;
using Microsoft.EntityFrameworkCore;
using SimpleDiplomBackend.Application.Shared.Extensions;
using SimpleDiplomBackend.Application.Shared.Interface;
using SimpleDiplomBackend.Application.Shared.Models;

namespace SimpleDiplomBackend.Application.Features.Application.Queries.GetAll
{
    public record GetAllQuery : IRequest<PaginatedList<ApplicationOptionDto>>
    {
        public int Offset { get; set; }
        public int Limit { get; set; }
    }

    public class GetAllQueryHandler : IRequestHandler<GetAllQuery, PaginatedList<ApplicationOptionDto>>
    {
        private readonly ISimpleDiplomBackendDbContext _dbContext;

        public GetAllQueryHandler(ISimpleDiplomBackendDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async ValueTask<PaginatedList<ApplicationOptionDto>> Handle(GetAllQuery request, CancellationToken cancellationToken)
        {
            var products = await _dbContext.ApplicationOptions
                .AsNoTracking()
                .Select(p => new ApplicationOptionDto
                {
                    Id = p.Id,
                    Value = p.Value
                })
                .OrderBy(p => p.Id)
                .PaginatedListAsync(request.Offset, request.Limit, cancellationToken);

            return products;
        }
    }
}