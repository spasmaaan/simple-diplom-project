using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleDiplomBackend.Application.Features.Dishes.Commands;
using SimpleDiplomBackend.Application.Features.Dishes.Commands.DeleteDish;
using SimpleDiplomBackend.Application.Features.Dishes.Commands.UpdateDish;
using SimpleDiplomBackend.Application.Features.Dishes.Queries.GetAllDishes;

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
            var query = new GetAllDishesQuery();
            var result = await _mediator.Send(query);

            return Ok(result);
        }

        //[Authorize(Policy = "ByUserKeyPolicy")]
        [HttpPost]
        [ApiVersion("1.0")]
        [Route("api/v{version:apiVersion}/services")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Create([FromBody] CreateServiceRequest request)
        {
            var command = new CreateDishCommand()
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
        public async Task<IActionResult> Update([FromBody] UpdateServiceRequest request)
        {
            var command = new UpdateDishCommand()
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
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteDishCommand()
            {
                Id = id
            };

            await _mediator.Send(command);

            return Ok();
        }

    }
}