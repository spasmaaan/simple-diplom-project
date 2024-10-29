using MediatR;
using Microsoft.EntityFrameworkCore;
using SimpleDiplomBackend.Application.Shared.Extensions;
using SimpleDiplomBackend.Application.Shared.Interface;
using SimpleDiplomBackend.Application.Shared.Models;

namespace SimpleDiplomBackend.Application.Features.Application.Queries.GetAll
{
    public record GetAllQuery : IRequest<Dictionary<string, string>>
    {
    }

    public class GetAllQueryHandler : IRequestHandler<GetAllQuery, Dictionary<string, string>>
    {
        private readonly ISimpleDiplomBackendDbContext _dbContext;

        public GetAllQueryHandler(ISimpleDiplomBackendDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Dictionary<string, string>> Handle(GetAllQuery request, CancellationToken cancellationToken)
        {
            var options = await _dbContext.ApplicationOptions
                .AsNoTracking().ToDictionaryAsync(x => x.Id, y => y.Value);

            return options;
        }
    }
}