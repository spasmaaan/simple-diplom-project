using Dapper;
using SimpleDiplomBackend.Application.Features.Application.Interfaces;
using SimpleDiplomBackend.Application.Features.Booking.Interfaces;
using SimpleDiplomBackend.Application.Features.Dishes.Interfaces;
using SimpleDiplomBackend.Application.Features.Photos.Interfaces;
using SimpleDiplomBackend.Application.Features.Services.Interfaces;
using SimpleDiplomBackend.Application.Shared.Interface;
using SimpleDiplomBackend.Domain.Entities;

namespace SimpleDiplomBackend.Persistence.Repositories
{
    public class PhotosRepository : IPhotosRepository
    {
        private readonly ISqlConnectionFactory _connectionFactory;

        public PhotosRepository(ISqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public Task Add(Photo photo)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Photo>> GetAll(int offset, int limit)
        {
            throw new NotImplementedException();
        }

        public Task<Photo?> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task Remove(int id)
        {
            throw new NotImplementedException();
        }

        public Task Update(Photo photo)
        {
            throw new NotImplementedException();
        }
    }
}