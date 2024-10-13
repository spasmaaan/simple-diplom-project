using MediatR;
using Microsoft.AspNetCore.Mvc;
using SimpleDiplomBackend.Application.Features.Photo.Commands.DeletePhoto;
using SimpleDiplomBackend.Application.Features.Photo.Commands.UpdatePhoto;
using SimpleDiplomBackend.Application.Features.Photo.Queries.GetAll;
using SimpleDiplomBackend.Booking.Features.Photo.Commands.CreatePhoto;

namespace SimpleDiplomBackend.Api.Endpoints.Photos
{
    [Produces("application/json")]
    [ApiController]
    public class PhotosController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PhotosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ApiVersion("1.0")]
        [Route("api/v{version:apiVersion}/photos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllQuery();
            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpPost]
        [ApiVersion("1.0")]
        [Route("api/v{version:apiVersion}/photos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Create([FromBody] CreatePhotoRequest request)
        {
            var command = new CreatePhotoCommand()
            {
                Image = Convert.FromBase64String(request.Image)
            };
            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpPut]
        [ApiVersion("1.0")]
        [Route("api/v{version:apiVersion}/photos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Update([FromBody] UpdatePhotoRequest request)
        {
            var command = new UpdatePhotoCommand()
            {
                Id = request.Id,
                Image = Convert.FromBase64String(request.Image),
            };
            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpDelete]
        [ApiVersion("1.0")]
        [Route("api/v{version:apiVersion}/photos/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeletePhotoCommand()
            {
                Id = id
            };

            await _mediator.Send(command);

            return Ok();
        }

    }
}