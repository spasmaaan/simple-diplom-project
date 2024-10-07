using SimpleDiplomBackend.Domain.Entities;

namespace SimpleDiplomBackend.Application.Features.Services.Interfaces
{
    public interface IServicesRepository
    {
        Task<CommercialService?> GetById(int id);
        Task<IEnumerable<CommercialService>> GetAll(int offset, int limit);
        Task Add(CommercialService service);
        Task Update(CommercialService service);
        Task Remove(int id);
    }
}