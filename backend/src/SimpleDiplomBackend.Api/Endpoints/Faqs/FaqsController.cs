
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleDiplomBackend.Api.Endpoints.Faqs;
using SimpleDiplomBackend.Application.Features.Faqs.Commands.DeleteFaq;
using SimpleDiplomBackend.Application.Features.Faqs.Commands.UpdateFaq;
using SimpleDiplomBackend.Application.Features.Faqs.Queries.GetAllFaqs;
using SimpleDiplomBackend.Booking.Features.Faqs.Commands.CreateFaq;

namespace SimpleDiplomBackend.Api.Endpoints.Faq
{
    [Produces("application/json")]
    [ApiController]
    public class FaqsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FaqsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        ///  Retrieves all FAQ information.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ApiVersion("1.0")]
        [Route("api/v{version:apiVersion}/faqs")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllFaqsQuery()
            {
                SearchTerm = "",
                Offset = 0,
                Limit = 10000,
            };
            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpPost]
        [ApiVersion("1.0")]
        [Route("api/v{version:apiVersion}/faqs")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateFaqRequest request)
        {
            var command = new CreateFaqCommand()
            {
                Question = request.Question,
                Answer = request.Answer,
            };
            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpPut]
        [ApiVersion("1.0")]
        [Route("api/v{version:apiVersion}/faqs")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize]
        public async Task<IActionResult> Update([FromBody] UpdateFaqRequest request)
        {
            var command = new UpdateFaqCommand()
            {
                Id = request.Id,
                Question = request.Question,
                Answer = request.Answer,
            };
            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpDelete]
        [ApiVersion("1.0")]
        [Route("api/v{version:apiVersion}/faqs/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteFaqCommand()
            {
                Id = id
            };

            await _mediator.Send(command);

            return Ok();
        }
    }
}