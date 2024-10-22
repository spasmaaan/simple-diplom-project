using MediatR;
using Microsoft.Extensions.Logging;

namespace SimpleDiplomBackend.Application.Shared.Behaviours
{
    public class LoggingBehaviour<TMessage, TResponse> : IPipelineBehavior<TMessage, TResponse>
         where TMessage : notnull
    {
        private readonly ILogger<TMessage> _logger;

        public LoggingBehaviour(ILogger<TMessage> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TMessage message, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var requestName = typeof(TMessage).Name;
            _logger.LogInformation("SimpleDiplomBackend Request: {name}, {@Request}", requestName, message);

            return await next();
        }
    }
}