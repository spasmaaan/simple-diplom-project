using MediatR;
using SimpleDiplomBackend.Application.Features.Booking.Interfaces;
using SimpleDiplomBackend.Application.Shared.Exceptions;

namespace SimpleDiplomBackend.Application.Features.Booking.Commands.UpdateBooking
{
    public record UpdateBookingCommand : IRequest<Domain.Entities.Booking>
    {
        public int Id { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? StatusId { get; set; }
        public Dictionary<int, int>? Dishes { get; set; }
        public Dictionary<int, int>? Services { get; set; }
    }

    public class UpdateBookingCommandHandler : IRequestHandler<UpdateBookingCommand, Domain.Entities.Booking>
    {
        private readonly IBookingRepository _bookingReporsitory;

        public UpdateBookingCommandHandler(IBookingRepository bookingRepository)
        {
            _bookingReporsitory = bookingRepository;
        }

        public async Task<Domain.Entities.Booking> Handle(UpdateBookingCommand request, CancellationToken cancellationToken)
        {
            var entity = await _bookingReporsitory.GetById(request.Id);
            if (entity == null)
            {
                throw new NotFoundException(nameof(Dishes), request.Id);
            }

            if (request.StartDate.HasValue)
            {
                entity.StartDate = request.StartDate.Value;
            }
            if (request.EndDate.HasValue)
            {
                entity.EndDate = request.EndDate.Value;
            }
            if (request.StatusId.HasValue)
            {
                entity.StatusId = request.StatusId.Value;
            }
            if (request.Dishes != null)
            {
                // entity.Dishes = request.Dishes;
            }
            if (request.Services != null)
            {
                // entity.Dishes = request.Dishes;
            }

            // update dish record
            await _bookingReporsitory.Update(entity);
            return entity;
        }
    }
}