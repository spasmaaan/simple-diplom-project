using MediatR;
using Microsoft.AspNetCore.Mvc;
using SimpleDiplomBackend.Application.Features.Booking.Commands.DeleteBooking;
using SimpleDiplomBackend.Application.Features.Booking.Commands.UpdateBooking;
using SimpleDiplomBackend.Application.Features.Booking.Queries.GetAll;
using SimpleDiplomBackend.Application.Features.Booking.Commands.CreateBooking;

namespace SimpleDiplomBackend.Api.Endpoints.Bookings
{
    [Produces("application/json")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BookingController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ApiVersion("1.0")]
        [Route("api/v{version:apiVersion}/bookings")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllQuery();
            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpPost]
        [ApiVersion("1.0")]
        [Route("api/v{version:apiVersion}/bookings")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Create([FromBody] CreateBookingRequest request)
        {
            var command = new CreateBookingCommand()
            {
                // TODO: ПОлучить юзера.
                UserId = "",
                StartDate = request.StartDate,
                EndDate = request.EndDate
            };
            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpPut]
        [ApiVersion("1.0")]
        [Route("api/v{version:apiVersion}/bookings")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Update([FromBody] UpdateBookingRequest request)
        {
            var command = new UpdateBookingCommand()
            {
                Id = request.Id,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                StatusId = request.StatusId,
                Dishes = request.Dishes,
                Services = request.Services,
            };
            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpDelete]
        [ApiVersion("1.0")]
        [Route("api/v{version:apiVersion}/bookings/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteBookingCommand()
            {
                Id = id
            };

            await _mediator.Send(command);

            return Ok();
        }

    }
}