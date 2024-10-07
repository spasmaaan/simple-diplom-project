using SimpleDiplomBackend.Application.Features.Services.Interfaces;
using SimpleDiplomBackend.Application.Shared.Interface;
using SimpleDiplomBackend.Domain.Entities;

namespace SimpleDiplomBackend.Persistence.Repositories
{
    public class ServicesRepository : IServicesRepository
    {
        private readonly ISqlConnectionFactory _connectionFactory;

        public ServicesRepository(ISqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public Task Add(CommercialService service)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CommercialService>> GetAll(int offset, int limit)
        {
            throw new NotImplementedException();
        }

        public Task<CommercialService?> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task Remove(int id)
        {
            throw new NotImplementedException();
        }

        public Task Update(CommercialService service)
        {
            throw new NotImplementedException();
        }
    }
}