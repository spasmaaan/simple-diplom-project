using SimpleDiplomBackend.Domain.Entities;

namespace SimpleDiplomBackend.Application.Features.Dishes.Interfaces
{
    public interface IDishRepository
    {
        Task<Dish?> GetById(int id);
        Task<IEnumerable<Dish>> GetAll(int offset, int limit);
        Task Add(Dish dish);
        Task Update(Dish dish);
        Task Remove(int id);

        Task<DishCategory?> GetCategoryById(int id);
        Task<IEnumerable<DishCategory>> GetAllCategories(int offset, int limit);
        Task AddCategory(DishCategory category);
        Task UpdateCategory(DishCategory category);
        Task RemoveCategory(int id);
    }
}