﻿using Mediator;
using SimpleDiplomBackend.Application.Shared.Interface;
using SimpleDiplomBackend.Domain.Entities;

namespace SimpleDiplomBackend.Application.Features.Dishes.Commands
{
    public record CreateDishCommand : IRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public byte[]? PreviewImage { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
    }

    public class CreateProductCommandHandler : IRequestHandler<CreateDishCommand>
    {
        private readonly ISimpleDiplomBackendDbContext _dbContext;

        public CreateProductCommandHandler(ISimpleDiplomBackendDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async ValueTask<Unit> Handle(CreateDishCommand request, CancellationToken cancellationToken)
        {

            var entity = new Dish
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                PreviewImage = request.PreviewImage,
                CategoryId = request.CategoryId
            };

            _dbContext.Dishes.Add(entity);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }

}