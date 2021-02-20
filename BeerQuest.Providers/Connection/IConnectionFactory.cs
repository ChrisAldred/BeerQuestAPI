using System.Data;

namespace BeerQuest.Providers.Connection
{
    public interface IConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}