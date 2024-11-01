using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SimpleDiplomBackend.Infrastructure.Auth;
using SimpleDiplomBackend.Application.Features.Authentication.Models;
using SimpleDiplomBackend.Application.Features.Authentication.Interfaces;
using SimpleDiplomBackend.Application.Shared.Enums;

namespace SimpleDiplomBackend.Infrastructure
{
    public static class DbInitializer
    {
        private class InitAppUser
        {
            public AppUser User { get; set; }
            public string Password { get; set; } = string.Empty;
            public string Role { get; set; } = string.Empty;

            public InitAppUser((string Email, string Password, string FirstName, string LastName, string Role) user)
            {
                User = new AppUser
                {
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName
                };
                Password = user.Password;
                Role = user.Role;
            }
        }

        private static readonly InitAppUser[] _users = (new (string, string, string, string, string)[]
            {
                ("admin@admin.ru", "ADMIN!admin1", "Админ", "Главный", UserRole.Admin),
                ("t1@t.test", "ADMIN!admin1", "Тестовый", "Пользователь", UserRole.Client),
                ("t2@t.test", "ADMIN!admin1", "Второй", "Тестовый", UserRole.Client),
                ("m@t.test", "ADMIN!admin1", "Сотрудник", "Пользователь", UserRole.Manager),
            })
            .Select(((string, string, string, string, string) user) => new InitAppUser(user))
            .ToArray();

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

            string[] roles = { UserRole.Admin, UserRole.Manager, UserRole.Client };
            foreach (var role in roles)
            {
                await authService.CreateRoleAsync(role);
            }

            foreach (var user in _users)
            {
                var result = await authService.RegisterUserAsync(user.User, user.Password);
                await authService.AddUserToRoleAsync(user.User.Email, user.Role);
            }
        }
    }
}