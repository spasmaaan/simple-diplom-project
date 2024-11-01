using MediatR;
using Microsoft.AspNetCore.Mvc;
using SimpleDiplomBackend.Application.Features.Booking.Commands.DeleteBooking;
using SimpleDiplomBackend.Application.Features.Booking.Commands.UpdateBooking;
using SimpleDiplomBackend.Application.Features.Booking.Queries.GetAll;
using SimpleDiplomBackend.Application.Features.Booking.Commands.CreateBooking;
using Microsoft.AspNetCore.Authorization;
using SimpleDiplomBackend.Application.Shared.Extensions;

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

        // Нужен метод для получения свободного расписания.

        [HttpGet]
        [ApiVersion("1.0")]
        [Route("api/v{version:apiVersion}/bookings")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllQuery
            {
                Offset = 1,
                Limit = 10000,
            };
            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpGet]
        [ApiVersion("1.0")]
        [Route("api/v{version:apiVersion}/bookings/freeTime")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllFreeTime()
        {
            var query = new GetAllFreeTimeQuery();
            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpPost]
        [ApiVersion("1.0")]
        [Route("api/v{version:apiVersion}/bookings")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateBookingRequest request)
        {
            var command = new CreateBookingCommand()
            {
                AccessToken = Request.GetBearerToken(),
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                Dishes = request.Dishes,
                Services = request.Services
            };
            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpPut]
        [ApiVersion("1.0")]
        [Route("api/v{version:apiVersion}/bookings")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize]
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
        [Authorize]
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