using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;

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
            
        }
    }
}