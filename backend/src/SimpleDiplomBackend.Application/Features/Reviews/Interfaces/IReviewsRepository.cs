namespace SimpleDiplomBackend.Application.Features.Photos.Interfaces
{
    public interface IReviewsRepository
    {
        Task<Domain.Entities.Review?> GetById(int id);
        Task<IEnumerable<Domain.Entities.Review>> GetAll(int offset, int limit);
        Task Add(Domain.Entities.Review review);
        Task Update(Domain.Entities.Review review);
        Task Remove(int id);
    }
}