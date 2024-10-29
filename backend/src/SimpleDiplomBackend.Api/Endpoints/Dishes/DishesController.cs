using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleDiplomBackend.Application.Features.Dishes.Commands;
using SimpleDiplomBackend.Application.Features.Dishes.Commands.DeleteDish;
using SimpleDiplomBackend.Application.Features.Dishes.Commands.UpdateDish;
using SimpleDiplomBackend.Application.Features.Dishes.Queries.GetAllDishCategories;
using SimpleDiplomBackend.Application.Features.Dishes.Queries.GetAllDishes;
using SimpleDiplomBackend.Application.Features.Dishes.Queries.GetCategoryImage;
using SimpleDiplomBackend.Application.Features.Dishes.Queries.GetDishImage;

namespace SimpleDiplomBackend.Api.Endpoints.Dishes
{
    [Produces("application/json")]
    [ApiController]
    public class DishesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DishesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ApiVersion("1.0")]
        [Route("api/v{version:apiVersion}/dishes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllDishes()
        {
            var query = new GetAllDishesQuery
            {
                Offset = 1,
                Limit = 10000,
            };
            var result = await _mediator.Send(query);

            return Ok(result);
        }


        [HttpGet]
        [ApiVersion("1.0")]
        [Route("api/v{version:apiVersion}/dishes/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetDishImage(int id)
        {
            var query = new GetDishImageQuery
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
        [Route("api/v{version:apiVersion}/dishes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize]
        public async Task<IActionResult> CreateDish([FromBody] CreateDishRequest request)
        {
            var command = new CreateDishCommand()
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                PreviewImage = Convert.FromBase64String(request.PreviewImage),
                CategoryId = request.CategoryId,
            };
            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpPut]
        [ApiVersion("1.0")]
        [Route("api/v{version:apiVersion}/dishes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize]
        public async Task<IActionResult> UpdateDish([FromBody] UpdateDishRequest request)
        {
            var command = new UpdateDishCommand()
            {
                Id = request.Id,
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                PreviewImage = Convert.FromBase64String(request.PreviewImage),
                CategoryId = request.CategoryId,
            };
            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpDelete]
        [ApiVersion("1.0")]
        [Route("api/v{version:apiVersion}/dishes/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize]
        public async Task<IActionResult> DeleteDish(int id)
        {
            var command = new DeleteDishCommand()
            {
                Id = id
            };

            await _mediator.Send(command);

            return Ok();
        }

        [HttpGet]
        [ApiVersion("1.0")]
        [Route("api/v{version:apiVersion}/dishes/categories")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllCategories()
        {
            var query = new GetAllDishCategoriesQuery
            {
                Offset = 1,
                Limit = 10000,
            };
            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpGet]
        [ApiVersion("1.0")]
        [Route("api/v{version:apiVersion}/dishes/categories/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCategoryImage(int id)
        {
            var query = new GetDishCategoryImageQuery
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
        [Route("api/v{version:apiVersion}/dishes/categories")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize]
        public async Task<IActionResult> CreateCategory([FromBody] CreateDishCategoryRequest request)
        {
            var command = new CreateDishCategoryCommand()
            {
                
                Name = request.Name,
                Description = request.Description,
                PreviewImage = Convert.FromBase64String(request.PreviewImage),
            };
            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpPut]
        [ApiVersion("1.0")]
        [Route("api/v{version:apiVersion}/dishes/categories")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize]
        public async Task<IActionResult> UpdateCategory([FromBody] UpdateDishCategoryRequest request)
        {
            var command = new UpdateDishCategoryCommand()
            {
                Id = request.Id,
                Name = request.Name,
                Description = request.Description,
                PreviewImage = Convert.FromBase64String(request.PreviewImage),
            };
            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpDelete]
        [ApiVersion("1.0")]
        [Route("api/v{version:apiVersion}/dishes/categories/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var command = new DeleteDishCategoryCommand()
            {
                Id = id
            };

            await _mediator.Send(command);

            return Ok();
        }
    }
}