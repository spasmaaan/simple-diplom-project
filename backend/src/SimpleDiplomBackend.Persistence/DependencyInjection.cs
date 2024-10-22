using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SimpleDiplomBackend.Application.Features.Faqs.Interfaces;
using SimpleDiplomBackend.Application.Features.Dishes.Interfaces;
using SimpleDiplomBackend.Application.Shared.Interface;
using SimpleDiplomBackend.Persistence.Repositories;

namespace SimpleDiplomBackend.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration,
            IWebHostEnvironment environment)
        {
            services.AddHealthChecks()
                .AddDbContextCheck<SimpleDiplomBackendDbContext>(name: "Application Database");

            // Register Dapper DbContext and Repositories
            services.AddSingleton<ISqlConnectionFactory, SqlConnectionFactory>();
            services.AddScoped<IDishRepository, DishRepository>();
            services.AddScoped<IFaqRepository, FaqRepository>();

            if (environment.IsProduction())
            {
                services.AddDbContext<SimpleDiplomBackendDbContext>(options =>
                    options.UseNpgsql(configuration.GetConnectionString("DatabaseConnection")));
                    //options.UseSqlite(configuration.GetConnectionString("DatabaseConnection")));
            }
            else
            {
                services.AddDbContext<SimpleDiplomBackendDbContext>(options =>
                    options.UseNpgsql(configuration.GetConnectionString("DatabaseConnection"))
                    //options.UseSqlite(configuration.GetConnectionString("DatabaseConnection"))
                        .LogTo(Console.WriteLine, LogLevel.Information));
            }

            services.AddScoped<ISimpleDiplomBackendDbContext>(provider =>
                provider.GetRequiredService<SimpleDiplomBackendDbContext>());

            return services;
        }
    }
}