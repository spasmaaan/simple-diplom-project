using MediatR;
using SimpleDiplomBackend.Application.Features.Authentication.Interfaces;
using SimpleDiplomBackend.Application.Shared.Exceptions;
using SimpleDiplomBackend.Application.Shared.Interface;
using SimpleDiplomBackend.Domain.Entities;
using SimpleDiplomBackend.Domain.Shared;

namespace SimpleDiplomBackend.Application.Features.Booking.Commands.CreateBooking
{
    public record CreateBookingCommand : IRequest<Domain.Entities.Booking>
    {
        public string AccessToken { get; set; } = string.Empty;
        public DateTime CreationDate { get; private set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Dictionary<int, int>? Dishes { get; set; }
        public Dictionary<int, int>? Services { get; set; }

        public CreateBookingCommand()
        {
            CreationDate = DateTime.Now;
        }

    }

    public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, Domain.Entities.Booking>
    {
        private readonly ISimpleDiplomBackendDbContext _dbContext;
        private readonly IJwtTokenService _jwtTokenService;

        public CreateBookingCommandHandler(ISimpleDiplomBackendDbContext dbContext, IJwtTokenService jwtTokenService)
        {
            _dbContext = dbContext;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<Domain.Entities.Booking> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
        {
            var user = await _jwtTokenService.GetUserByTokenAsync(request.AccessToken, cancellationToken);

            var entity = new Domain.Entities.Booking
            {
                UserId = user!.Id,
                CreationDate = request.CreationDate,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                StatusId = (int)BookingStatusType.Processing,
            };

            var added = _dbContext.Bookings.Add(entity);

            if (request.Dishes != null) {
                _dbContext.BookingDishes.AddRange(request.Dishes.Select(x => new BookingDish
                {
                    BookingId = entity.Id,
                    DishId = x.Key,
                    Count = x.Value
                }));
            }

            if (request.Services != null)
            {
                _dbContext.BookingServices.AddRange(request.Services.Select(x => new BookingService
                {
                    BookingId = entity.Id,
                    ServiceId = x.Key,
                    Count = x.Value
                }));
            }

            await _dbContext.SaveChangesAsync(cancellationToken);

            return added.Entity;
        }
    }

}