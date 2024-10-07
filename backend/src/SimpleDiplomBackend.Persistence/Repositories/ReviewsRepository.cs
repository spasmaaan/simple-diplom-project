using SimpleDiplomBackend.Application.Features.Photos.Interfaces;
using SimpleDiplomBackend.Application.Shared.Interface;
using SimpleDiplomBackend.Domain.Entities;

namespace SimpleDiplomBackend.Persistence.Repositories
{
    public class ReviewsRepository : IReviewsRepository
    {
        private readonly ISqlConnectionFactory _connectionFactory;

        public ReviewsRepository(ISqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public Task Add(Review review)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Review>> GetAll(int offset, int limit)
        {
            throw new NotImplementedException();
        }

        public Task<Review?> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task Remove(int id)
        {
            throw new NotImplementedException();
        }

        public Task Update(Review review)
        {
            throw new NotImplementedException();
        }
    }
}