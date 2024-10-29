using MediatR;
using SimpleDiplomBackend.Application.Features.Authentication.Interfaces;
using SimpleDiplomBackend.Application.Shared.Exceptions;

namespace SimpleDiplomBackend.Application.Features.Authentication.Commands.Logout
{
    public record LogoutCommand : IRequest
    {
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
    }

    public class LogoutCommandHandler : IRequestHandler<LogoutCommand>
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IJwtTokenService _jwtTokenService;
        public LogoutCommandHandler(IAuthenticationService authenticationService, IJwtTokenService jwtTokenService)
        {
            _authenticationService = authenticationService;
            _jwtTokenService = jwtTokenService;
        }

        public async Task Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            var result = await _jwtTokenService.RevokeTokenAsync(request.AccessToken, request.RefreshToken, cancellationToken);
            if (!result.Succeeded)
            {
                throw new UnauthorizedException(result.Error);
            }
            return;
        }
    }
}