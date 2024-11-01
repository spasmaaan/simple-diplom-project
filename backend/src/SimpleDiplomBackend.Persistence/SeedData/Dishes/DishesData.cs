using Microsoft.EntityFrameworkCore;
using SimpleDiplomBackend.Application.Shared.Utilities;
using SimpleDiplomBackend.Domain.Entities;

namespace SimpleDiplomBackend.Persistence.SeedData.Dishes
{
    internal static class DishesData
    {
        private static readonly Dish[] _dishes = (new (int, int, string, string, decimal, string)[]
            {
                (1, 1, "Цезарь", "Салат Цезарь с перепелиным яйцом.", 430, "1.jpg"),
                (2, 1, "Греческий", "Греческий салат с фетой.", 320, "2.jpg"),
                (3, 1, "Поке", "Поке с лососем.", 450, "3.jpg"),
                (4, 2, "Стандартный", "Сочный бургер с картошечкой.", 370, "4.jpg"),
                (5, 2, "Двухкотлетный", "Две котлеты, много сыра, чёрные булки.", 520, "5.jpg"),
                (6, 2, "Сырный", "Не котлета с сыром, а сыр с котлетой.", 420, "6.jpg"),
                (7, 3, "Маргарита", "Классическая пицца на тонком тесте. Диаметр 32см.", 670, "7.jpg"),
                (8, 3, "Пеперони", "Острая пицца на тонком тесте. Диаметр 32см.", 780, "8.jpg"),
                (9, 3, "Барбекю", "Пицца на пышном тесте. Диаметр 32см.", 810, "9.jpg"),
                (10, 3, "Ранч", "Пицца на пышном тесте. Диаметр 32см.", 790, "10.jpg"),
                (11, 4, "Чёрный чай", "Подаётся в чайничке объёмом 1л.", 350, "11.jpg"),
                (12, 4, "Цветочный чай анчан", "Синего цвета. Подаётся в чайничке объёмом 1л.", 380, "12.jpg"),
                (13, 4, "Облепиховый чай", "Чай с соком и ягодоами облепихи. Подаётся в чайничке объёмом 1л.", 420, "13.jpg"),
                (14, 4, "Двойной эспрессо", "Порция 60 мл.", 120, "14.jpg"),
                (15, 4, "Капучино", "Порция 300 мл.", 210, "15.jpg"),
                (16, 4, "Макиато", "Порция 300 мл.", 260, "16.jpg"),
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
