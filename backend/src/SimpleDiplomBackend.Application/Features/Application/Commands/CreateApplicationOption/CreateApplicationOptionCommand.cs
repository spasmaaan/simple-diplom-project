using MediatR;
using SimpleDiplomBackend.Application.Shared.Interface;
using SimpleDiplomBackend.Domain.Entities;

namespace SimpleDiplomBackend.Application.Features.Application.Commands
{
    public record CreateApplicationOptionCommand : IRequest
    {
        public required string Id { get; set; }
        public string Values { get; set; } = string.Empty;
    }

    public class CreateApplicationOptionCommandHandler : IRequestHandler<CreateApplicationOptionCommand>
    {
        private readonly ISimpleDiplomBackendDbContext _dbContext;

        public CreateApplicationOptionCommandHandler(ISimpleDiplomBackendDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(CreateApplicationOptionCommand request, CancellationToken cancellationToken)
        {
            var entity = new ApplicationOption
            {
                Id = request.Id,
                Value = request.Values,
            };

            _dbContext.ApplicationOptions.Add(entity);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }

}