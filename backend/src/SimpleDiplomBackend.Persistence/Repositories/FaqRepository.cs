using Dapper;
using SimpleDiplomBackend.Application.Features.Faqs.Interfaces;
using SimpleDiplomBackend.Application.Shared.Interface;
using SimpleDiplomBackend.Domain.Entities;

namespace SimpleDiplomBackend.Persistence.Repositories
{
    public class FaqRepository : IFaqRepository
    {
        private readonly ISqlConnectionFactory _connectionFactory;

        public FaqRepository(ISqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public Task Add(Faq faq)
        {
            using var connection = _connectionFactory.CreateConnection();
            connection.ExecuteAsync(
                @"INSERT INTO Faqs
                  VALUES (@Question, @Answer)", new { faq.Question, faq.Answer });

            return Task.CompletedTask;
        }

        public async Task<IEnumerable<Faq>> GetAll(int offset = 1, int limit = 10)
        {
            using var connection = _connectionFactory.CreateConnection();
            var faqs = await connection.QueryAsync<Faq>(
                @"SELECT Id, Question, Answer
                 FROM Faqs
                 ORDER BY Question ASC
                 OFFSET @offset ROWS FETCH NEXT @limit ONLY",
                new { offset, limit });

            return faqs;
        }

        public async Task<Faq> GetById(int id)
        {
            using var connection = _connectionFactory.CreateConnection();
            var faq = await connection.QueryFirstOrDefaultAsync<Faq>(
                @"SELECT Id, Question, Answer
                  FROM Faqs
                  WHERE Id = @id ", new { id });

            return faq!;
        }

        public Task Update(Faq faq)
        {
            using var connection = _connectionFactory.CreateConnection();
            connection.ExecuteAsync(
                @"UPDATE Faqs
                  VALUES (@Question, @Answer)", new { faq.Question, faq.Answer });

            return Task.CompletedTask;
        }

        public Task Remove(int id)
        {
            using var connection = _connectionFactory.CreateConnection();
            connection.ExecuteAsync(
               @"DELETE FROM Faqs
                 WHERE Id = @id", new { id });

            return Task.CompletedTask;
        }
    }
}