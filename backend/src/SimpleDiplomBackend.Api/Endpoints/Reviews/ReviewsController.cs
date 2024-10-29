using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleDiplomBackend.Application.Features.Review.Commands.CreateReview;
using SimpleDiplomBackend.Application.Features.Review.Commands.DeleteReview;
using SimpleDiplomBackend.Application.Features.Review.Commands.UpdateReview;
using SimpleDiplomBackend.Application.Features.Review.Queries.GetAll;

namespace SimpleDiplomBackend.Api.Endpoints.Reviews
{
    [Produces("application/json")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReviewsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ApiVersion("1.0")]
        [Route("api/v{version:apiVersion}/reviews")]
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

        [HttpPost]
        [ApiVersion("1.0")]
        [Route("api/v{version:apiVersion}/reviews")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateReviewRequest request)
        {
            var command = new CreateReviewCommand()
            {
                Message = request.Message,
                Rating = request.Rating,
            };
            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpPut]
        [ApiVersion("1.0")]
        [Route("api/v{version:apiVersion}/reviews")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize]
        public async Task<IActionResult> Update([FromBody] UpdateReviewRequest request)
        {
            var command = new UpdateReviewCommand()
            {
                Id = request.Id,
                Message = request.Message,
                Rating = request.Rating,
            };
            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpDelete]
        [ApiVersion("1.0")]
        [Route("api/v{version:apiVersion}/reviews/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteReviewCommand()
            {
                Id = id
            };

            await _mediator.Send(command);

            return Ok();
        }

    }
}