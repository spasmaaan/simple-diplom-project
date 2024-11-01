using MediatR;
using Microsoft.EntityFrameworkCore;
using SimpleDiplomBackend.Application.Features.Booking.Queries.GetAllFreeTime;
using SimpleDiplomBackend.Application.Shared.Extensions;
using SimpleDiplomBackend.Application.Shared.Interface;
using SimpleDiplomBackend.Application.Shared.Models;

namespace SimpleDiplomBackend.Application.Features.Booking.Queries.GetAll
{
    public record GetAllFreeTimeQuery : IRequest<List<BookingFreeTimeDto>>
    {
        public int Offset { get; set; }
        public int Limit { get; set; }
    }

    public class GetAllFreeTimeHandler : IRequestHandler<GetAllFreeTimeQuery, List<BookingFreeTimeDto>>
    {
        private readonly ISimpleDiplomBackendDbContext _dbContext;

        public GetAllFreeTimeHandler(ISimpleDiplomBackendDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<BookingFreeTimeDto>> Handle(GetAllFreeTimeQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.Bookings
                .AsNoTracking()
                .OrderBy(p => p.StartDate)
                .Where(p => p.StatusId == 1)
                .Select(p => new BookingFreeTimeDto
                {
                    StartDate = p.StartDate,
                    EndDate = p.EndDate
                }).ToListAsync();
        }
    }
}