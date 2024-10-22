using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace SimpleDiplomBackend.Persistence
{
    public class SimpleDiplomBackendDbContextFactory : IDesignTimeDbContextFactory<SimpleDiplomBackendDbContext>
    {
        private const string ConnectionStringName = "DatabaseConnection";
        private const string AspNetCoreEnvironment = "ASPNETCORE_ENVIRONMENT";

        public SimpleDiplomBackendDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<SimpleDiplomBackendDbContext>();
            optionsBuilder.UseNpgsql(GetConnectionString());
            //optionsBuilder.UseSqlite(GetConnectionString());

            return new SimpleDiplomBackendDbContext(optionsBuilder.Options);
        }

        private static string GetConnectionString()
        {
            var basePath = Directory.GetCurrentDirectory();

            var environmentName = Environment.GetEnvironmentVariable(AspNetCoreEnvironment);
            Console.WriteLine(environmentName);

            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.Local.json", optional: true)
                .AddJsonFile($"appsettings.{environmentName}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            Console.WriteLine(configuration.GetConnectionString(ConnectionStringName));

            var connectionString = configuration.GetConnectionString(ConnectionStringName);
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentException($"Connection string '{ConnectionStringName}' is null or empty.", nameof(connectionString));
            }

            return connectionString;
        }
    }
}