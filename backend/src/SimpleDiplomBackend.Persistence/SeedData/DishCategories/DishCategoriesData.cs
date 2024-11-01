using Microsoft.EntityFrameworkCore;
using SimpleDiplomBackend.Application.Shared.Utilities;
using SimpleDiplomBackend.Domain.Entities;

namespace SimpleDiplomBackend.Persistence.SeedData.DishCategories
{
    internal static class DishCategoriesData
    {
        private static readonly DishCategory[] _dishCategories = (new (int, string, string, string)[]
            {
                (1, "Салаты", "Мало калорий", "1.jpg"),
                (2, "Бургеры", "Для тех кто голоден", "2.jpg"),
                (3, "Пиццы", "Для утончённых ценителей", "3.jpg"),
                (4, "Напитки", "Всё, чем можно запить", "4.jpg"),
            })
            .Select(((int Id, string Name, string Description, string FileName) dish) => new DishCategory
            {
                Id = dish.Id,
                Name = dish.Name,
                Description = dish.Description,
                PreviewMimeType = DataFileLoaderUtilities.GetFileMimeType(dish.FileName),
                PreviewImage = DataFileLoaderUtilities.GetFileData($"SeedData/{nameof(DishCategories)}/Images/{dish.FileName}")
            })
            .ToArray();

        public async static Task FillData(this DbSet<DishCategory> dbDishCategories)
        {
            await dbDishCategories.AddRangeAsync(_dishCategories);
        }
    }
}
