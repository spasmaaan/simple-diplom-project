using MediatR;
using SimpleDiplomBackend.Application.Features.Authentication.Interfaces;

namespace SimpleDiplomBackend.Application.Features.Authentication.Commands.CreateRole
{
    public record CreateRoleCommand : IRequest
    {
        public string RoleName { get; set; } = string.Empty;
    }

    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand>
    {
        private readonly IAuthenticationService _authenticationService;

        public CreateRoleCommandHandler(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public async Task Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            await _authenticationService.CreateRoleAsync(request.RoleName);
        }
    }
}
