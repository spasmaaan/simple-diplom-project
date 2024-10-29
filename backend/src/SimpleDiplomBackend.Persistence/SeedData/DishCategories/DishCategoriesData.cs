using Microsoft.EntityFrameworkCore;
using SimpleDiplomBackend.Application.Shared.Utilities;
using SimpleDiplomBackend.Domain.Entities;

namespace SimpleDiplomBackend.Persistence.SeedData.DishCategories
{
    internal static class DishCategoriesData
    {
        private static readonly DishCategory[] _dishCategories = (new (int, string, string, string)[]
            {
                (1, "Итальянская кухня", "Описание 1", "1.jpg"),
                (2, "Заморская кухня", "Описание 2", "2.jpg"),
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
