using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using SimpleDiplomBackend.Persistence.SeedData.ApplicationOptions;
using SimpleDiplomBackend.Persistence.SeedData.Bookings;
using SimpleDiplomBackend.Persistence.SeedData.CommercialServices;
using SimpleDiplomBackend.Persistence.SeedData.DishCategories;
using SimpleDiplomBackend.Persistence.SeedData.Dishes;
using SimpleDiplomBackend.Persistence.SeedData.Faqs;
using SimpleDiplomBackend.Persistence.SeedData.Photos;
using SimpleDiplomBackend.Persistence.SeedData.Reviews;

namespace SimpleDiplomBackend.Persistence
{
    public static class DbInitializer
    {
        public static IApplicationBuilder UseInitializePersistanceDatabase(this IApplicationBuilder application, bool doReinitDatabase)
        {
            using var serviceScope = application.ApplicationServices.CreateScope();
            var dbContext = serviceScope.ServiceProvider.GetService<SimpleDiplomBackendDbContext>();

            if (dbContext != null && doReinitDatabase)
            {
                ReinitDatabase(serviceScope, dbContext).Wait();
            }

            return application;
        }

        public static async Task ReinitDatabase(IServiceScope serviceScope, SimpleDiplomBackendDbContext dbContext)
        {
            var databaseCreator = dbContext.GetService<IRelationalDatabaseCreator>();
            await databaseCreator.CreateTablesAsync();

            await SeedData(serviceScope, dbContext);
        }

        public static async Task SeedData(IServiceScope serviceScope, SimpleDiplomBackendDbContext dbContext)
        {
            await dbContext.ApplicationOptions.FillData();
            await dbContext.CommercialServices.FillData();
            await dbContext.DishCategories.FillData();
            await dbContext.Dishes.FillData();
            await dbContext.Faqs.FillData();
            await dbContext.Photos.FillData();
            await dbContext.BookingStatuses.FillData();
            await dbContext.Reviews.FillData();
            await dbContext.Bookings.FillData();
            await dbContext.BookingDishes.FillData();
            await dbContext.BookingServices.FillData();

            CancellationTokenSource source = new CancellationTokenSource();
            CancellationToken cancellationToken = source.Token;
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}