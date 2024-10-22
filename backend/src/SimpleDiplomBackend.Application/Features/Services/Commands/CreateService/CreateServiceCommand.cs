using MediatR;
using SimpleDiplomBackend.Application.Shared.Interface;
using SimpleDiplomBackend.Domain.Entities;

namespace SimpleDiplomBackend.Booking.Features.Service.Commands.CreateService
{
    public record CreateServiceCommand : IRequest<CommercialService>
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public byte[]? PreviewImage { get; set; }
        public decimal Price { get; set; }
    }

    public class CreateServiceCommandHandler : IRequestHandler<CreateServiceCommand, CommercialService>
    {
        private readonly ISimpleDiplomBackendDbContext _dbContext;

        public CreateServiceCommandHandler(ISimpleDiplomBackendDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CommercialService> Handle(CreateServiceCommand request, CancellationToken cancellationToken)
        {
            var entity = new CommercialService
            {
                Name = request.Name,
                Description = request.Description,
                PreviewImage = request.PreviewImage,
                Price = request.Price
            };

            var added = _dbContext.CommercialServices.Add(entity);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return added.Entity;
        }
    }
}