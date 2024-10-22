using MediatR;
using Microsoft.EntityFrameworkCore;
using SimpleDiplomBackend.Application.Shared.Extensions;
using SimpleDiplomBackend.Application.Shared.Interface;
using SimpleDiplomBackend.Application.Shared.Models;

namespace SimpleDiplomBackend.Application.Features.Review.Queries.GetAll
{
    public record GetAllQuery : IRequest<PaginatedList<ReviewDto>>
    {
        public int Offset { get; set; }
        public int Limit { get; set; }
    }

    public class GetAllQueryHandler : IRequestHandler<GetAllQuery, PaginatedList<ReviewDto>>
    {
        private readonly ISimpleDiplomBackendDbContext _dbContext;

        public GetAllQueryHandler(ISimpleDiplomBackendDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PaginatedList<ReviewDto>> Handle(GetAllQuery request, CancellationToken cancellationToken)
        {
            var products = await _dbContext.Reviews
                .AsNoTracking()
                .Select(p => new ReviewDto
                {
                    Id = p.Id,
                    UserId = p.UserId,
                    UserName = "User name",
                    Message = p.Message,
                    Rating = p.Rating,
                    CreationDate = p.CreationDate,               
                })
                .OrderByDescending(p => p.CreationDate)
                .PaginatedListAsync(request.Offset, request.Limit, cancellationToken);

            return products;
        }
    }
}