using Mediator;
using SimpleDiplomBackend.Application.Shared.Interface;
using SimpleDiplomBackend.Domain.Entities;

namespace SimpleDiplomBackend.Application.Features.Dishes.Commands
{
    public record CreateApplicationOptionCommand : IRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }

    public class CreateApplicationOptionCommandHandler : IRequestHandler<CreateApplicationOptionCommand>
    {
        private readonly ISimpleDiplomBackendDbContext _dbContext;

        public CreateApplicationOptionCommandHandler(ISimpleDiplomBackendDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async ValueTask<Unit> Handle(CreateApplicationOptionCommand request, CancellationToken cancellationToken)
        {

            var entity = new ApplicationOption
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price
            };

            _dbContext.Dishes.Add(entity);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }

}