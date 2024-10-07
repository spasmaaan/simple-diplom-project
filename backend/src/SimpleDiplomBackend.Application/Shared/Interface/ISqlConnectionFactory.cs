using System.Data;

namespace SimpleDiplomBackend.Application.Shared.Interface
{
    public interface ISqlConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}