using MediatR;
using SimpleDiplomBackend.Application.Shared.Exceptions;
using SimpleDiplomBackend.Application.Shared.Interface;

namespace SimpleDiplomBackend.Application.Features.Booking.Commands.CreateBooking
{
    public record CreateBookingCommand : IRequest<Domain.Entities.Booking>
    {
        public string UserId { get; set; } = string.Empty;
        public DateTime CreationDate { get; private set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public CreateBookingCommand()
        {
            CreationDate = DateTime.Now;
        }

    }

    public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, Domain.Entities.Booking>
    {
        private readonly ISimpleDiplomBackendDbContext _dbContext;

        public CreateBookingCommandHandler(ISimpleDiplomBackendDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Domain.Entities.Booking> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
        {
            var entity = new Domain.Entities.Booking
            {
                UserId = request.UserId,
                CreationDate = request.CreationDate,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                StatusId = 0,
            };

            var added = _dbContext.Bookings.Add(entity);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return added.Entity;
        }
    }

}