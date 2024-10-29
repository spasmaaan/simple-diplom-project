using MediatR;
using Microsoft.EntityFrameworkCore;
using SimpleDiplomBackend.Application.Shared.Interface;
using SimpleDiplomBackend.Domain.Entities;

namespace SimpleDiplomBackend.Application.Features.Dishes.Queries.GetDishImage
{
    public record GetDishImageQuery : IRequest<DishImageDto?>
    {
        public int Id { get; set; }
    }

    public class GetDishImageQueryHandler : IRequestHandler<GetDishImageQuery, DishImageDto?>
    {
        private readonly ISimpleDiplomBackendDbContext _dbContext;

        public GetDishImageQueryHandler(ISimpleDiplomBackendDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<DishImageDto?> Handle(GetDishImageQuery request, CancellationToken cancellationToken)
        {
            Dish? dish = await _dbContext.Dishes
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == request.Id);
            return dish != null 
                ? new DishImageDto
                { 
                    Data = dish.PreviewImage ?? Array.Empty<byte>(),
                    MimeType = dish.PreviewMimeType,
                }
                : null;
        }
    }
}