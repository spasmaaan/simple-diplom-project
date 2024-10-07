using Microsoft.Extensions.Logging;
using SimpleDiplomBackend.Application.Shared.Interface;
using SimpleDiplomBackend.Domain.Entities;
using SimpleDiplomBackend.Domain.Shared;
using Newtonsoft.Json;

namespace SimpleDiplomBackend.Infrastructure.Outboxes
{
    public class OutboxService(ISimpleDiplomBackendDbContext dbContext, 
        ILogger<OutboxService> logger) 
        : IOutboxService
    {
        private readonly ISimpleDiplomBackendDbContext _dbContext = dbContext;
        private readonly ILogger<OutboxService> _logger = logger;

        public async Task StoreDomainEvent(IDomainEvent domainEvent, 
            CancellationToken cancellationToken)
        {
            await _dbContext.OutboxMessages.AddAsync(new OutboxMessage
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                Type = domainEvent.GetType().Name,
                Payload = JsonConvert.SerializeObject(
                    domainEvent,
                    new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.All
                    })
            }, cancellationToken);

            try
            {
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to save domain event to outbox: {Message}", ex.Message);
                throw;
            }
        }
    }
}