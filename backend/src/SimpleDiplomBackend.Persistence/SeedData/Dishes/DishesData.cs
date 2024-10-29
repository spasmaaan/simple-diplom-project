using Microsoft.EntityFrameworkCore;
using SimpleDiplomBackend.Application.Shared.Utilities;
using SimpleDiplomBackend.Domain.Entities;

namespace SimpleDiplomBackend.Persistence.SeedData.Dishes
{
    internal static class DishesData
    {
        private static readonly Dish[] _dishes = (new (int, int, string, string, decimal, string)[]
            {
                (1, 1, "Блюдо 1", "Описание блюда 1", 8, "1.jpg"),
                (2, 1, "Блюдо 2", "Описание блюда 2", 32, "2.jpg"),
                (3, 2, "Блюдо 3", "Описание блюда 3", 41, "3.jpg"),
            })
            .Select(((int Id, int CategoryId, string Name, string Description, decimal Price, string FileName) dish) => new Dish
            {
                Id = dish.Id,
                CategoryId = dish.CategoryId,
                Name = dish.Name,
                Description = dish.Description,
                Price = dish.Price,
                PreviewMimeType = DataFileLoaderUtilities.GetFileMimeType(dish.FileName),
                PreviewImage = DataFileLoaderUtilities.GetFileData($"SeedData/{nameof(Dishes)}/Images/{dish.FileName}")
            })
            .ToArray();

        public async static Task FillData(this DbSet<Dish> dbDishes)
        {
            await dbDishes.AddRangeAsync(_dishes);
        }
    }
}
