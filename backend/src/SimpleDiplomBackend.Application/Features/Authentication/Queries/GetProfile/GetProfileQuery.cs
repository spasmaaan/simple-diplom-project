using MediatR;
using SimpleDiplomBackend.Application.Features.Authentication.Interfaces;
using SimpleDiplomBackend.Application.Features.Authentication.Models;
using SimpleDiplomBackend.Application.Shared.Exceptions;
using SimpleDiplomBackend.Application.Shared.Interface;
using System.Data;

namespace SimpleDiplomBackend.Application.Features.Authentication.Queries.GetProfile
{
    public record GetProfileQuery : IRequest<UserInfo>, IAccessTokenRequest
    {
        public string AccessToken { get; set; } = string.Empty;
    }

    public class GetProfileQueryHandler : IRequestHandler<GetProfileQuery, UserInfo>
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IJwtTokenService _jwtTokenService;
        public GetProfileQueryHandler(IAuthenticationService authenticationService, IJwtTokenService jwtTokenService)
        {
            _authenticationService = authenticationService;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<UserInfo> Handle(GetProfileQuery request, CancellationToken cancellationToken)
        {
            var user = await _jwtTokenService.GetUserByTokenAsync(request.AccessToken, cancellationToken);         
            return user!;
        }
    }
}