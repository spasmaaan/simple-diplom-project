using MediatR;
using Microsoft.EntityFrameworkCore;
using SimpleDiplomBackend.Application.Features.Authentication.Interfaces;
using SimpleDiplomBackend.Application.Features.Authentication.Models;
using SimpleDiplomBackend.Application.Shared.Extensions;
using SimpleDiplomBackend.Application.Shared.Interface;
using SimpleDiplomBackend.Application.Shared.Models;
using System.Linq;

namespace SimpleDiplomBackend.Application.Features.Review.Queries.GetAll
{
    public record GetAllQuery : IRequest<PaginatedList<ReviewDto>>
    {
        public int Offset { get; set; }
        public int Limit { get; set; }
    }

    internal record UserReviewInfo
    {
        public string ShortName { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
    }

    public class GetAllQueryHandler : IRequestHandler<GetAllQuery, PaginatedList<ReviewDto>>
    {
        private readonly ISimpleDiplomBackendDbContext _dbContext;
        private readonly IAuthenticationService _authenticationService;

        public GetAllQueryHandler(ISimpleDiplomBackendDbContext dbContext, IAuthenticationService authenticationService)
        {
            _dbContext = dbContext;
            _authenticationService = authenticationService;
        }

        public async Task<PaginatedList<ReviewDto>> Handle(GetAllQuery request, CancellationToken cancellationToken)
        {
            Dictionary<string, UserReviewInfo> users = (await _authenticationService.GetUsersAsync()).ToDictionary(x => x.Id, y => new UserReviewInfo {
                FullName = $"{y.FirstName} {y.LastName}",
                ShortName = $"{y.FirstName.FirstOrDefault()}{y.LastName.FirstOrDefault()}"
            });
            var products = await _dbContext.Reviews
                .AsNoTracking()
                .Select(p => new ReviewDto
                    {
                        Id = p.Id,
                        ShortName = users.ContainsKey(p.UserId) ? users[p.UserId].ShortName : string.Empty,
                        FullName = users.ContainsKey(p.UserId) ? users[p.UserId].FullName : string.Empty,
                        Message = p.Message,
                        Rating = p.Rating,
                        CreationDate = p.CreationDate,
                        // FIXME: Исправить потом.
                        Me = true,
                    })
                .OrderByDescending(p => p.CreationDate)
                .PaginatedListAsync(request.Offset, request.Limit, cancellationToken);

            return products;
        }
    }
}