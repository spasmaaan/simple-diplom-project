using Dapper;
using SimpleDiplomBackend.Application.Features.Dishes.Interfaces;
using SimpleDiplomBackend.Application.Shared.Interface;
using SimpleDiplomBackend.Domain.Entities;

namespace SimpleDiplomBackend.Persistence.Repositories
{
    public class DishRepository : IDishRepository
    {
        private readonly ISqlConnectionFactory _connectionFactory;

        public DishRepository(ISqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public Task Add(Dish dish)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Dish>> GetAll(int offset, int limit)
        {
            throw new NotImplementedException();
        }

        public async Task<Dish?> GetById(int id)
        {
            var sql = @"SELECT ID, Name, Description, Price
                        FROM Dishes p
                        INNER JOIN Categories pc ON p.ID
                        WHERE ID = @Id"
            ;

            using var sqlconnection = _connectionFactory.CreateConnection();
            var entity = await sqlconnection.QueryAsync<Dish, DishCategory, Dish>
                (sql, (product, productCategory) =>
                {
                    // product.Catergory = productCategory;
                    return product;
                },
                splitOn: "ID");

            if (entity == null)
            {
                return null;
            }

            return entity.FirstOrDefault();
        }

        public Task Remove(int id)
        {
            throw new NotImplementedException();
        }

        public async Task Update(Dish product)
        {
            using var sqlconnection = _connectionFactory.CreateConnection();
            await sqlconnection.ExecuteAsync(
                @"UPDATE Dishes
                 SET @Name, @Description, @Price
                 WHERE ID = @Id",
                new { product.Id, product.Name, product.Description, product.Price });
        }

        public async Task<DishCategory?> GetCategoryById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<DishCategory>> GetAllCategories(int offset, int limit)
        {
            throw new NotImplementedException();
        }
        public async Task AddCategory(DishCategory category)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateCategory(DishCategory category)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveCategory(int id)
        {
            throw new NotImplementedException();
        }
    }
}