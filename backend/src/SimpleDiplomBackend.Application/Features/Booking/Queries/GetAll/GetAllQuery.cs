using MediatR;
using Microsoft.EntityFrameworkCore;
using SimpleDiplomBackend.Application.Shared.Extensions;
using SimpleDiplomBackend.Application.Shared.Interface;
using SimpleDiplomBackend.Application.Shared.Models;

namespace SimpleDiplomBackend.Application.Features.Booking.Queries.GetAll
{
    public record GetAllQuery : IRequest<PaginatedList<BookingDto>>
    {
        public int Offset { get; set; }
        public int Limit { get; set; }
    }

    public class GetAllQueryHandler : IRequestHandler<GetAllQuery, PaginatedList<BookingDto>>
    {
        private readonly ISimpleDiplomBackendDbContext _dbContext;

        public GetAllQueryHandler(ISimpleDiplomBackendDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PaginatedList<BookingDto>> Handle(GetAllQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.Bookings
                .AsNoTracking()
                .Select(p => new BookingDto
                {
                    Id = p.Id,
                    UserId = p.UserId, // FIXME: Only god
                    CreationDate = p.CreationDate,
                    StartDate = p.StartDate,
                    EndDate = p.EndDate,
                    StatusId = p.StatusId,
                    // TODO: Добавить извлечение dishes.
                    Dishes = new Dictionary<int, int>(),
                    // TODO: Добавить извлечение services.
                    Services = new Dictionary<int, int>()
                })
                .OrderBy(p => p.StartDate)
                .PaginatedListAsync(request.Offset, request.Limit, cancellationToken);
        }
    }
}