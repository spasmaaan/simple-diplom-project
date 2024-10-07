using Mediator;
using SimpleDiplomBackend.Application.Shared.Interface;
using SimpleDiplomBackend.Domain.Entities;

namespace SimpleDiplomBackend.Booking.Features.Service.Commands.CreateService
{
    public record CreateServiceCommand : IRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public byte[]? PreviewImage { get; set; }
        public decimal Price { get; set; }
    }

    public class CreateServiceCommandHandler : IRequestHandler<CreateServiceCommand>
    {
        private readonly ISimpleDiplomBackendDbContext _dbContext;

        public CreateServiceCommandHandler(ISimpleDiplomBackendDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async ValueTask<Unit> Handle(CreateServiceCommand request, CancellationToken cancellationToken)
        {

            var entity = new CommercialService
            {
                Name = request.Name,
                Description = request.Description,
                PreviewImage = request.PreviewImage,
                Price = request.Price
            };

            _dbContext.CommercialServices.Add(entity);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }

}