using Mediator;
using SimpleDiplomBackend.Application.Features.Application.Interfaces;
using SimpleDiplomBackend.Application.Shared.Exceptions;

namespace SimpleDiplomBackend.Application.Features.Application.Commands.UpdateApplicationOption
{
    public record UpdateApplicationOptionCommand : IRequest
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }

    public class UpdateApplicationOptionCommandHandler : IRequestHandler<UpdateApplicationOptionCommand>
    {
        private readonly IApplicationRepository _applicationReporsitory;

        public UpdateApplicationOptionCommandHandler(IApplicationRepository dishRepository)
        {
            _applicationReporsitory = dishRepository;
        }

        public async ValueTask<Unit> Handle(UpdateApplicationOptionCommand request, CancellationToken cancellationToken)
        {

            var entity = await _applicationReporsitory.GetById(request.Id);
            // check if dish was found.
            if (entity == null)
            {
                throw new NotFoundException(nameof(Dishes), request.Id);
            }

            // update dish record
            await _applicationReporsitory.Update(entity);

            return Unit.Value;

        }
    }
}