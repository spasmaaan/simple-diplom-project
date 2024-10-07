namespace SimpleDiplomBackend.Application.Features.Photos.Interfaces
{
    public interface IPhotosRepository
    {
        Task<Domain.Entities.Photo?> GetById(int id);
        Task<IEnumerable<Domain.Entities.Photo>> GetAll(int offset, int limit);
        Task Add(Domain.Entities.Photo photo);
        Task Update(Domain.Entities.Photo photo);
        Task Remove(int id);
    }
}