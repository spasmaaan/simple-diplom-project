using SimpleDiplomBackend.Domain.Entities;

namespace SimpleDiplomBackend.Application.Features.Faqs.Interfaces
{
    public interface IFaqRepository
    {
        Task Add(Domain.Entities.Faq faq);
        Task<Domain.Entities.Faq> GetById(int id);
        Task<IEnumerable<Domain.Entities.Faq>> GetAll(int offset, int limit);
        Task Update(Domain.Entities.Faq faq);
        Task Remove(int id);
    }
}