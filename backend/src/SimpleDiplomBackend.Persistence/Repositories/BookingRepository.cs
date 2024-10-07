using SimpleDiplomBackend.Application.Features.Booking.Interfaces;
using SimpleDiplomBackend.Application.Shared.Interface;

namespace SimpleDiplomBackend.Persistence.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly ISqlConnectionFactory _connectionFactory;

        public BookingRepository(ISqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public Task Add(Domain.Entities.Booking booking)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Domain.Entities.Booking>> GetAll(int offset, int limit)
        {
            throw new NotImplementedException();
        }

        public Task<Domain.Entities.Booking?> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task Remove(int id)
        {
            throw new NotImplementedException();
        }

        public Task Update(Domain.Entities.Booking booking)
        {
            throw new NotImplementedException();
        }
    }
}