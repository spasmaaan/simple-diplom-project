using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SimpleDiplomBackend.Application.Shared.Interface;
using System.Data;

namespace SimpleDiplomBackend.Persistence
{
    public sealed class SqlConnectionFactory : ISqlConnectionFactory
    {
        private readonly IConfiguration _configuration;

        public SqlConnectionFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection CreateConnection()
        {
            return new SqlConnection(_configuration.GetConnectionString("DatabaseConnection"));
        }
    }
}