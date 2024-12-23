﻿
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleDiplomBackend.Application.Features.Authentication.Commands.AddClaimToUser;
using SimpleDiplomBackend.Application.Features.Authentication.Commands.AddUserToRole;
using SimpleDiplomBackend.Application.Features.Authentication.Commands.CreateRole;
using SimpleDiplomBackend.Application.Features.Authentication.Commands.Login;
using SimpleDiplomBackend.Application.Features.Authentication.Commands.Logout;
using SimpleDiplomBackend.Application.Features.Authentication.Commands.RefreshToken;
using SimpleDiplomBackend.Application.Features.Authentication.Commands.RegisterUser;
using SimpleDiplomBackend.Application.Features.Authentication.Commands.RemoveUserFromRole;
using SimpleDiplomBackend.Application.Features.Authentication.Queries.GetAllRoles;
using SimpleDiplomBackend.Application.Features.Authentication.Queries.GetProfile;
using SimpleDiplomBackend.Application.Features.Authentication.Queries.GetUserClaims;
using SimpleDiplomBackend.Application.Features.Authentication.Queries.GetUserRoles;
using SimpleDiplomBackend.Application.Shared.Extensions;
using System.Net;
using System.Text.RegularExpressions;

namespace SimpleDiplomBackend.Api.Endpoints.Auth
{
    [Produces("application/json")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        ///  Authenticate user's login credentials
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [ApiVersion("1.0")]
        [Route("api/v{version:apiVersion}/auth/login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var command = new LoginCommand()
            {
                Email = request.Email.Trim(),
                Password = request.Password.Trim()
            };

            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Refresh JWT when expired.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [ApiVersion("1.0")]
        [Route("api/v{version:apiVersion}/auth/refresh")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize]
        public async Task<IActionResult> Refresh(RefreshTokenRequest request)
        {
            var command = new RefreshTokenCommand()
            {
                AccessToken = request.AccessToken.Trim(),
                RefreshToken = request.RefreshToken.Trim()
            };

            var result = await _mediator.Send(command);

            return Ok(result);
        }

        /// <summary>
        /// Register user.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [ApiVersion("1.0")]
        [Route("api/v{version:apiVersion}/auth/register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var command = new RegisterUserCommand
            {
                FirstName = request.FirstName.Trim(),
                LastName = request.LastName.Trim(),
                Email = request.Email.Trim(),
                Password = request.Password.Trim()
            };

            await _mediator.Send(command);
            return Ok();
        }


        /// <summary>
        /// Logout user.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ApiVersion("1.0")]
        [Route("api/v{version:apiVersion}/auth/logout")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            var command = new LogoutCommand()
            {
                AccessToken = Request.GetBearerToken(),
            };

            await _mediator.Send(command);
            return Ok();
        }

        /// <summary>
        /// Get current user info.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ApiVersion("1.0")]
        [Route("api/v{version:apiVersion}/auth/profile")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize]
        public async Task<IActionResult> CurrentUser()
        {
            var command = new GetProfileQuery()
            {
                AccessToken = Request.GetBearerToken(),
            };

            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Get all user roles
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ApiVersion("1.0")]
        [Route("api/v{version:apiVersion}/auth/roles")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize]
        public async Task<IActionResult> GetAllRoles()
        {
            var query = new GetAllRolesQuery();
            var result = await _mediator.Send(query);

            return Ok(result);
        }

        /// <summary>
        /// Create user role
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        [HttpPost]
        [ApiVersion("1.0")]
        [Route("api/v{version:apiVersion}/auth/roles")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize]
        public async Task<IActionResult> CreateRole([FromBody] string roleName)
        {
            var command = new CreateRoleCommand
            {
                RoleName = roleName.Trim()
            };

            await _mediator.Send(command);
            return Ok();
        }


        /// <summary>
        /// Add role to user
        /// </summary>
        /// <param name="email"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        [HttpPost]
        [ApiVersion("1.0")]
        [Route("api/v{version:apiVersion}/auth/roles/users")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize]
        public async Task<IActionResult> AddUserToRole(string email, string roleName)
        {
            var command = new AddUserToRoleCommand
            {
                Email = email.Trim(),
                RoleName = roleName.Trim()
            };

            await _mediator.Send(command);
            return Ok();
        }

        /// <summary>
        /// Get user's roles 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpGet]
        [ApiVersion("1.0")]
        [Route("api/v{version:apiVersion}/auth/roles/users")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize]
        public async Task<IActionResult> GetUserRoles(string email)
        {
            var query = new GetUserRolesQuery
            {
                Email = email.Trim()
            };

            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Remove role from user.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        [HttpDelete]
        [ApiVersion("1.0")]
        [Route("api/v{version:apiVersion}/auth/roles/users")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize]
        public async Task<IActionResult> RemoveUserFromRole(string email, string roleName)
        {
            var command = new RemoveUserFromRoleCommand
            {
                Email = email.Trim(),
                RoleName = roleName.Trim()
            };

            await _mediator.Send(command);
            return Ok();
        }

        /// <summary>
        /// Get user's claims
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpGet]
        [ApiVersion("1.0")]
        [Route("api/v{version:apiVersion}/auth/claims/users")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize]
        public async Task<IActionResult> GetUserClaims(string email)
        {
            var query = new GetUserClaimsQuery
            {
                Email = email.Trim()
            };

            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Add claim to user.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="claimName"></param>
        /// <param name="claimValue"></param>
        /// <returns></returns>
        [HttpPost]
        [ApiVersion("1.0")]
        [Route("api/v{version:apiVersion}/auth/claims/users")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize]
        public async Task<IActionResult> AddClaimToUser(string email, string claimName, string claimValue)
        {
            var command = new AddClaimToUserCommand
            {
                Email = email.Trim(),
                ClaimName = claimName.Trim(),
                ClaimValue = claimValue.Trim()
            };

            await _mediator.Send(command);
            return Ok();
        }
    }
}