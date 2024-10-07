using SimpleDiplomBackend.Domain.Shared;

namespace SimpleDiplomBackend.Application.Shared.Interface
{
    public interface IOutboxService
    {
        Task StoreDomainEvent(IDomainEvent domainEvent, CancellationToken cancellationToken);
    }
}