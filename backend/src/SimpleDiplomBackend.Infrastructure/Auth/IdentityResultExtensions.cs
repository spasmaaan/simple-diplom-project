﻿using Microsoft.AspNetCore.Identity;
using SimpleDiplomBackend.Application.Features.Authentication.Models;

namespace SimpleDiplomBackend.Infrastructure.Auth
{
    public static class IdentityResultextensions
    {
        public static Result MapToResult(this IdentityResult result)
        {
            return result.Succeeded
                ? Result.Success()
                : Result.Failure(result.Errors.Select(e => e.Description));
        }

        public static Result MapToResult(this SignInResult result)
        {
            return result.Succeeded
                ? Result.Success()
                : Result.Failure(new string[] { "Invalid login attempt." });
        }
    }
}