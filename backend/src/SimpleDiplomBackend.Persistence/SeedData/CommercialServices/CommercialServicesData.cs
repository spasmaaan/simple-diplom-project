using Microsoft.EntityFrameworkCore;
using SimpleDiplomBackend.Application.Shared.Utilities;
using SimpleDiplomBackend.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SimpleDiplomBackend.Persistence.SeedData.CommercialServices
{
    internal static class CommercialServicesData
    {
        private static readonly CommercialService[] _services = (new (int, string, string, decimal, int, string)[]
            {
                (1, "Лепестки роз", "Количество 1 л.", 400, 10, "1.jpg"),
                (2, "Свечи на стол", "Свечи, создающие романтическую обстановку. Цена за 1 шт.", 50, 10, "2.jpg"),
                (3, "Воздушные шары", "Романтические шары с гелием. Цена за 1 шт.", 200, 30, "3.jpg"),
                (4, "Фотосессия", "В стоимость входит работа фотографа в течении 2-х оборотов колеса.", 2000, 1, "4.jpg"),
            })
            .Select(((int Id, string Name, string Description, decimal Price, int MaxCount, string FileName) dish) => new CommercialService
            {
                Id = dish.Id,
                Name = dish.Name,
                Description = dish.Description,
                Price = dish.Price,
                MaxCount = dish.MaxCount,
                PreviewMimeType = DataFileLoaderUtilities.GetFileMimeType(dish.FileName),
                PreviewImage = DataFileLoaderUtilities.GetFileData($"SeedData/{nameof(CommercialServices)}/Images/{dish.FileName}")
            })
            .ToArray();

        public async static Task FillData(this DbSet<CommercialService> dbServices)
        {
            await dbServices.AddRangeAsync(_services);
        }
    }
}
