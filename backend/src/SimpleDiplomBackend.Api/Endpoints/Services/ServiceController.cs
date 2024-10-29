using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleDiplomBackend.Application.Features.Service.Commands.DeleteService;
using SimpleDiplomBackend.Application.Features.Service.Commands.UpdateService;
using SimpleDiplomBackend.Application.Features.Service.Queries.GetAll;
using SimpleDiplomBackend.Application.Features.Services.Queries.GetServiceImage;
using SimpleDiplomBackend.Booking.Features.Service.Commands.CreateService;

namespace SimpleDiplomBackend.Api.Endpoints.Services
{
    [Produces("application/json")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ServiceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ApiVersion("1.0")]
        [Route("api/v{version:apiVersion}/services")]
        [ProducesResponseType(StatusCodes.Status200OK)]
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
        [Route("api/v{version:apiVersion}/services/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetServiceImage(int id)
        {
            var query = new GetServiceImageQuery
            {
                Id = id
            };
            var result = await _mediator.Send(query);

            if (result == null)
            {
                return Ok();
            }

            return new FileStreamResult(
                new MemoryStream(result.Data), result.MimeType
            );
        }

        [HttpPost]
        [ApiVersion("1.0")]
        [Route("api/v{version:apiVersion}/services")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateServiceRequest request)
        {
            var command = new CreateServiceCommand()
            {
                Name = request.Name,
                Description = request.Description,
                PreviewImage = Convert.FromBase64String(request.PreviewImage),
                Price = request.Price
            };
            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpPut]
        [ApiVersion("1.0")]
        [Route("api/v{version:apiVersion}/services")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize]
        public async Task<IActionResult> Update([FromBody] UpdateServiceRequest request)
        {
            var command = new UpdateServiceCommand()
            {
                Id = request.Id,
                Name = request.Name,
                Description = request.Description,
                PreviewImage = Convert.FromBase64String(request.PreviewImage),
                Price = request.Price
            };
            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpDelete]
        [ApiVersion("1.0")]
        [Route("api/v{version:apiVersion}/services/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteServiceCommand()
            {
                Id = id
            };

            await _mediator.Send(command);

            return Ok();
        }
    }
}