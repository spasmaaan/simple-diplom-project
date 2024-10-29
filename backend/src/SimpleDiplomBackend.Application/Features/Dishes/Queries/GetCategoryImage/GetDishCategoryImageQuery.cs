using MediatR;
using Microsoft.EntityFrameworkCore;
using SimpleDiplomBackend.Application.Shared.Interface;
using SimpleDiplomBackend.Domain.Entities;

namespace SimpleDiplomBackend.Application.Features.Dishes.Queries.GetCategoryImage
{
    public record GetDishCategoryImageQuery : IRequest<DishCategoryImageDto?>
    {
        public int Id { get; set; }
    }

    public class GetDishCategoryImageQueryHandler : IRequestHandler<GetDishCategoryImageQuery, DishCategoryImageDto?>
    {
        private readonly ISimpleDiplomBackendDbContext _dbContext;

        public GetDishCategoryImageQueryHandler(ISimpleDiplomBackendDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<DishCategoryImageDto?> Handle(GetDishCategoryImageQuery request, CancellationToken cancellationToken)
        {
            DishCategory? dishCategory = await _dbContext.DishCategories
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == request.Id);
            return dishCategory != null 
                ? new DishCategoryImageDto
                { 
                    Data = dishCategory.PreviewImage ?? Array.Empty<byte>(),
                    MimeType = dishCategory.PreviewMimeType,
                }
                : null;
        }
    }
}