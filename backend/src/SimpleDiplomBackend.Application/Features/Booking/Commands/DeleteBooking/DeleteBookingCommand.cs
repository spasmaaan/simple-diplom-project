using MediatR;
using SimpleDiplomBackend.Application.Shared.Exceptions;
using SimpleDiplomBackend.Application.Shared.Interface;

namespace SimpleDiplomBackend.Application.Features.Booking.Commands.DeleteBooking
{
    public record DeleteBookingCommand : IRequest<Domain.Entities.Booking>
    {
        public long Id { get; set; }
    }

    public class DeleteBookingCommandHandler : IRequestHandler<DeleteBookingCommand, Domain.Entities.Booking>
    {
        private readonly ISimpleDiplomBackendDbContext _dbContext;

        public DeleteBookingCommandHandler(ISimpleDiplomBackendDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Domain.Entities.Booking> Handle(DeleteBookingCommand request, CancellationToken cancellationToken)
        {
            var entity = _dbContext.Bookings.FirstOrDefault(p => p.Id == request.Id);

            if (entity == null)
            {
                throw new NotFoundException();
            }
            
            var removed = _dbContext.Bookings.Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return removed.Entity;
        }
    }
}