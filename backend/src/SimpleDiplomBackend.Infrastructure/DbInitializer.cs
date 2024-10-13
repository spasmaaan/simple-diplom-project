using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SimpleDiplomBackend.Infrastructure.Auth;

namespace SimpleDiplomBackend.Infrastructure
{
    public static class DbInitializer
    {
        public static IApplicationBuilder UseInitializeInfrastructureDatabase(this IApplicationBuilder application)
        {
            using var serviceScope = application.ApplicationServices.CreateScope();
            var dbContext = serviceScope.ServiceProvider.GetService<ApplicationIdentityDbContext>();

            // only call this method when there are pending migrations
            if (dbContext != null && false)
            {
                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();
            }

            // TODO: Add method for database seeding
            // SeedData(dbContext);

            return application;
        }
    }
}