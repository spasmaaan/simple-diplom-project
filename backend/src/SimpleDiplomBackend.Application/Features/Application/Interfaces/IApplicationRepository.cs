using SimpleDiplomBackend.Domain.Entities;

namespace SimpleDiplomBackend.Application.Features.Application.Interfaces
{
    public interface IApplicationRepository
    {
        Task<ApplicationOption?> GetById(string id);
        Task<IEnumerable<ApplicationOption>> GetAll(int offset, int limit);
        Task Add(ApplicationOption applicationOption);
        Task Update(ApplicationOption applicationOption);
        Task Remove(string id);
    }
}