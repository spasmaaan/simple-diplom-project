using Microsoft.AspNetCore.Http;
using SimpleDiplomBackend.Application.Shared.Exceptions;
using System.Text.RegularExpressions;

namespace SimpleDiplomBackend.Application.Shared.Extensions
{
    public static class HttpHeadersExtension
    {
        private const string AuthHeaderName = "Authorization";

        public static string GetBearerToken(this HttpRequest request)
        {  
            if (!request.Headers.ContainsKey(AuthHeaderName))
            {
                throw new UnauthorizedException($"{AuthHeaderName} http header not found!");
            }
            // Тут не удаляется bearer.
            string result = Regex.Replace(request.Headers[AuthHeaderName].ToString(), "^Bearer\\s+", string.Empty, RegexOptions.IgnoreCase).Trim();

            return result;
        }
    }
}
