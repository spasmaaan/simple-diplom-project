using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using SimpleDiplomBackend.Infrastructure.Auth;
using System.Runtime.CompilerServices;
using System;
using Microsoft.CodeAnalysis;

namespace SimpleDiplomBackend.Infrastructure
{
    public static class DbInitializer
    {
        public static IApplicationBuilder UseInitializeInfrastructureDatabase(this IApplicationBuilder application)
        {
            using IServiceScope serviceScope = application.ApplicationServices.CreateScope();
            ApplicationIdentityDbContext? dbContext = serviceScope.ServiceProvider.GetService<ApplicationIdentityDbContext>();

            // only call this method when there are pending migrations
            if (dbContext != null && true)
            {
                ReinitDatabase(serviceScope, dbContext).Wait();
            }

            // TODO: Add method for database seeding
            // SeedData(dbContext);

            return application;
        }

        public static string NormalizeKey(string key)
        {
            return key.ToUpperInvariant();
        }

        public static async Task ReinitDatabase(IServiceScope serviceScope, ApplicationIdentityDbContext dbContext)
        {
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();
            await SeedData(serviceScope, dbContext);
        }

        public static async Task SeedData(IServiceScope serviceScope, ApplicationIdentityDbContext dbContext)
        {
            string[] roles = { "admin", "manager", "client" };

            var user = new ApplicationUser
            {
                FirstName = "admin",
                LastName = "admin",
                Email = "admin@admin.ru",
                NormalizedEmail = NormalizeKey("admin@admin.ru"),
                UserName = "admin",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };

            await SeedRoles(dbContext, roles);
            await AddAdminUser(serviceScope, dbContext, user, "admin", roles);
        }

        public static async Task SeedRoles(ApplicationIdentityDbContext dbContext, string[] roles)
        {
            var roleStore = new RoleStore<IdentityRole>(dbContext);
            foreach (string role in roles)
            {
                if (!dbContext.Roles.Any(r => r.Name == role))
                {
                    await roleStore.CreateAsync(new IdentityRole
                    {
                        Name = role,
                        NormalizedName = NormalizeKey(role)
                    });
                }
            }
            await dbContext.SaveChangesAsync();
        }

        public static string CreatePasswordHash(ApplicationUser user, string password)
        {
            var passwordHasher = new PasswordHasher<ApplicationUser>();
            return passwordHasher.HashPassword(user, password);
        }

        public static async Task AddAdminUser(IServiceScope serviceScope, ApplicationIdentityDbContext dbContext, ApplicationUser user, string password, string[] roles)
        {
            var userStore = new UserStore<ApplicationUser>(dbContext);
            if (!dbContext.Users.Any(u => u.UserName == user.UserName))
            {
                user.PasswordHash = CreatePasswordHash(user, password);
                IdentityResult result = await userStore.CreateAsync(user);
            }
            await dbContext.SaveChangesAsync();

            await AssignRoles(serviceScope, dbContext, user.Email!, roles);
        }

        public static async Task AssignRoles(IServiceScope serviceScope, ApplicationIdentityDbContext dbContext, string email, string[] roles)
        {
            UserManager<ApplicationUser> userManager = serviceScope.ServiceProvider.GetService<UserManager<ApplicationUser>>()!;
            ApplicationUser user = (await userManager.FindByEmailAsync(NormalizeKey(email)))!;

            foreach (var role in roles)
            {
                if (!await userManager.IsInRoleAsync(user, role))
                {
                    await userManager.AddToRoleAsync(user, role);
                }
            }

            await dbContext.SaveChangesAsync();
        }
    }
}