using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleDiplomBackend.Api.Endpoints.Application;
using SimpleDiplomBackend.Application.Features.Application.Queries.GetAll;

namespace SimpleDiplomBackend.Api.Endpoints.Bookings
{
    [Produces("application/json")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ApplicationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ApiVersion("1.0")]
        [Route("api/v{version:apiVersion}/application/options")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllQuery();
            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpPut]
        [ApiVersion("1.0")]
        [Route("api/v{version:apiVersion}/application/options")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize]
        public async Task<IActionResult> Update([FromBody] UpdateApplicationOptionRequest request)
        {
            var command = new UpdateApplicationOptionRequest()
            {
                Id = request.Id,
                Value = request.Value,
            };
            var result = await _mediator.Send(command);

            return Ok(result);
        }
    }
}