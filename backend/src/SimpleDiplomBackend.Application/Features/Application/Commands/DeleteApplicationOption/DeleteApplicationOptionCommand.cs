using MediatR;
using SimpleDiplomBackend.Application.Shared.Exceptions;
using SimpleDiplomBackend.Application.Shared.Interface;

namespace SimpleDiplomBackend.Application.Features.Application.Commands.DeleteApplicationOption
{
    public record DeleteApplicationOptionCommand : IRequest
    {
        public long Id { get; set; }
    }

    public class DeleteApplicationOptionCommandHandler : IRequestHandler<DeleteApplicationOptionCommand>
    {
        private readonly ISimpleDiplomBackendDbContext _dbContext;

        public DeleteApplicationOptionCommandHandler(ISimpleDiplomBackendDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(DeleteApplicationOptionCommand request, CancellationToken cancellationToken)
        {
            var entity = _dbContext.Dishes.FirstOrDefault(p => p.Id == request.Id);

            if (entity == null)
            {
                throw new NotFoundException();
            }

            _dbContext.Dishes.Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);

        }
    }
}