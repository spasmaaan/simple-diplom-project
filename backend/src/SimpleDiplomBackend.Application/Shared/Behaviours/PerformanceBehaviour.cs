using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace SimpleDiplomBackend.Application.Shared.Behaviours
{
    public class PerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
    {
        private readonly Stopwatch _timer;
        private readonly ILogger<TRequest> _logger;

        public PerformanceBehaviour(ILogger<TRequest> logger)
        {
            _timer = new Stopwatch();
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest message, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            _timer.Start();

            var response = await next();
            var elapsedMilliseconds = _timer.ElapsedMilliseconds;

            // any request taking longer than 2 seconds should be reported
            if (elapsedMilliseconds > 2000)
            {
                var requestName = typeof(TRequest).Name;

                _logger.LogWarning("Long running request: {name} ({elapsedMilliseconds} milliseconds) {@Request}]",
                    requestName, elapsedMilliseconds, message);
            }

            return response;
        }
    }
}