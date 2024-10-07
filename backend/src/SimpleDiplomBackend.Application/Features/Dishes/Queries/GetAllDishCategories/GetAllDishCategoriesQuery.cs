using Mediator;
using Microsoft.EntityFrameworkCore;
using SimpleDiplomBackend.Application.Shared.Extensions;
using SimpleDiplomBackend.Application.Shared.Interface;
using SimpleDiplomBackend.Application.Shared.Models;

namespace SimpleDiplomBackend.Application.Features.Dishes.Queries.GetAllDishCategories
{
    public record GetAllDishCategoriesQuery : IRequest<PaginatedList<DishCategoryDto>>
    {
        public int Offset { get; set; }
        public int Limit { get; set; }
    }

    public class GetAllDishCategoriesHandler : IRequestHandler<GetAllDishCategoriesQuery, PaginatedList<DishCategoryDto>>
    {
        private readonly ISimpleDiplomBackendDbContext _dbContext;

        public GetAllDishCategoriesHandler(ISimpleDiplomBackendDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async ValueTask<PaginatedList<DishCategoryDto>> Handle(GetAllDishCategoriesQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.DishCategories
                .AsNoTracking()
                .Select(p => new DishCategoryDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    PreviewImage = p.PreviewImage,              
                })
                .OrderBy(p => p.Name)
                .PaginatedListAsync(request.Offset, request.Limit, cancellationToken);
        }
    }
}