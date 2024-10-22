using MediatR;
using SimpleDiplomBackend.Application.Features.Application.Interfaces;
using SimpleDiplomBackend.Application.Shared.Exceptions;

namespace SimpleDiplomBackend.Application.Features.Application.Commands.UpdateApplicationOption
{
    public record UpdateApplicationOptionCommand : IRequest
    {
        public required string Id { get; set; }
        public string? Value { get; set; } = string.Empty;
    }

    public class UpdateApplicationOptionCommandHandler : IRequestHandler<UpdateApplicationOptionCommand>
    {
        private readonly IApplicationRepository _applicationReporsitory;

        public UpdateApplicationOptionCommandHandler(IApplicationRepository dishRepository)
        {
            _applicationReporsitory = dishRepository;
        }

        public async Task Handle(UpdateApplicationOptionCommand request, CancellationToken cancellationToken)
        {

            var entity = await _applicationReporsitory.GetById(request.Id);
            // check if dish was found.
            if (entity == null)
            {
                throw new NotFoundException(nameof(Application), request.Id);
            }

            if (request.Value != null)
            {
                entity.Value = request.Value;
            }

            // update dish record
            await _applicationReporsitory.Update(entity);
        }
    }
}