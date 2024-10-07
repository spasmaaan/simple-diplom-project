using SimpleDiplomBackend.Application.Features.Application.Interfaces;
using SimpleDiplomBackend.Application.Shared.Interface;
using SimpleDiplomBackend.Domain.Entities;

namespace SimpleDiplomBackend.Persistence.Repositories
{
    public class ApplicationRepository : IApplicationRepository
    {
        private readonly ISqlConnectionFactory _connectionFactory;

        public ApplicationRepository(ISqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public Task Add(ApplicationOption applicationOption)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ApplicationOption>> GetAll(int offset, int limit)
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationOption?> GetById(string id)
        {
            throw new NotImplementedException();
        }

        public Task Remove(string id)
        {
            throw new NotImplementedException();
        }

        public Task Update(ApplicationOption applicationOption)
        {
            throw new NotImplementedException();
        }
    }
}