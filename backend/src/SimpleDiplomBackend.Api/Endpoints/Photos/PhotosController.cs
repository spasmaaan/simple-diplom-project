using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleDiplomBackend.Application.Features.Photo.Commands.DeletePhoto;
using SimpleDiplomBackend.Application.Features.Photo.Commands.UpdatePhoto;
using SimpleDiplomBackend.Application.Features.Photo.Queries.GetAll;
using SimpleDiplomBackend.Application.Features.Photos.Queries.GetImage;
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
            var query = new GetAllQuery {
                Offset = 1,
                Limit = 10000,
            };
            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpGet]
        [ApiVersion("1.0")]
        [Route("api/v{version:apiVersion}/photos/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetImage(int id)
        {
            var query = new GetImageQuery
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
        [Route("api/v{version:apiVersion}/photos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreatePhotoRequest request)
        {
            var command = new CreatePhotoCommand()
            {
                ImageBase64 = request.Image
            };
            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpPut]
        [ApiVersion("1.0")]
        [Route("api/v{version:apiVersion}/photos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize]
        public async Task<IActionResult> Update([FromBody] UpdatePhotoRequest request)
        {
            var command = new UpdatePhotoCommand()
            {
                Id = request.Id,
                ImageBase64 = request.Image
            };
            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpDelete]
        [ApiVersion("1.0")]
        [Route("api/v{version:apiVersion}/photos/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize]
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