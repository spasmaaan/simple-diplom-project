using Mediator;
using Microsoft.EntityFrameworkCore;
using SimpleDiplomBackend.Application.Shared.Extensions;
using SimpleDiplomBackend.Application.Shared.Interface;
using SimpleDiplomBackend.Application.Shared.Models;

namespace SimpleDiplomBackend.Application.Features.Dishes.Queries.GetAllDishes
{
    public record GetAllDishesQuery : IRequest<PaginatedList<DishDto>>
    {
        public int Offset { get; set; }
        public int Limit { get; set; }
    }

    public class GetAllQueryHandler : IRequestHandler<GetAllDishesQuery, PaginatedList<DishDto>>
    {
        private readonly ISimpleDiplomBackendDbContext _dbContext;

        public GetAllQueryHandler(ISimpleDiplomBackendDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async ValueTask<PaginatedList<DishDto>> Handle(GetAllDishesQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.Dishes
                .AsNoTracking()
                .Select(p => new DishDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    PreviewImage = p.PreviewImage,
                    Price = p.Price,
                    CategoryId = p.CategoryId                   
                })
                .OrderBy(p => p.Name)
                .PaginatedListAsync(request.Offset, request.Limit, cancellationToken);
        }
    }
}