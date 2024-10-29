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
        private static readonly CommercialService[] _services = (new (int, string, string, decimal, string)[]
            {
                (1, "Сервис 1", "Описание сервиса 1", 10, "1.jpg"),
                (2, "Сервис 2", "Описание сервиса 2", 12, "2.jpg"),
            })
            .Select(((int Id, string Name, string Description, decimal Price, string FileName) dish) => new CommercialService
            {
                Id = dish.Id,
                Name = dish.Name,
                Description = dish.Description,
                Price = dish.Price,
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
