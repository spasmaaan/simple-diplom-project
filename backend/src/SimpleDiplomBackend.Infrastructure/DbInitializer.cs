using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SimpleDiplomBackend.Infrastructure.Auth;
using SimpleDiplomBackend.Application.Features.Authentication.Models;
using SimpleDiplomBackend.Application.Features.Authentication.Interfaces;

namespace SimpleDiplomBackend.Infrastructure
{
    public static class DbInitializer
    {
        public static IApplicationBuilder UseInitializeInfrastructureDatabase(this IApplicationBuilder application, bool doReinitDatabase)
        {
            if (doReinitDatabase)
            {
                ReinitDatabase(application);
                SeedData(application).Wait();
            }

            return application;
        }

        public static void ReinitDatabase(IApplicationBuilder application)
        {
            using IServiceScope serviceScope = application.ApplicationServices.CreateScope();
            ApplicationIdentityDbContext dbContext = serviceScope.ServiceProvider.GetService<ApplicationIdentityDbContext>()!;

            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();
        }

        public static async Task SeedData(IApplicationBuilder application)
        {
            using IServiceScope serviceScope = application.ApplicationServices.CreateScope();
            IAuthenticationService authService = serviceScope.ServiceProvider.GetService<IAuthenticationService>()!;

            string[] roles = { "admin", "manager", "client" };
            
            foreach (var role in roles)
            {
                await authService.CreateRoleAsync(role);
            }

            var user = new AppUser
            {
                FirstName = "admin",
                LastName = "admin",
                Email = "admin@admin.ru",
            };
            var result = await authService.RegisterUserAsync(user, "ADMIN!admin1");

            await authService.AddUserToRoleAsync(user.Email, "admin");

        }
    }
}