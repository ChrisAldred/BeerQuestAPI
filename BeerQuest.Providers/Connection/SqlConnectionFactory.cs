using System;
using System.Data;
using System.Data.SqlClient;

namespace BeerQuest.Providers.Connection
{
    public class SqlConnectionFactory : IConnectionFactory
    {
        private readonly IConnectionContextFactory _connectionContextFactory;
        private readonly IConnectionStringBuilder _connectionStringBuilder;

        public SqlConnectionFactory(IConnectionContextFactory connectionContextFactory, IConnectionStringBuilder connectionStringBuilder)
        {
            _connectionContextFactory = connectionContextFactory ?? throw new ArgumentNullException(nameof(connectionContextFactory));
            _connectionStringBuilder = connectionStringBuilder ?? throw new ArgumentNullException(nameof(connectionStringBuilder));
        }

        public IDbConnection CreateConnection()
        {
            var connectionContext = _connectionContextFactory.GetCurrentConnectionContext();
            if (connectionContext == null)
            {
                throw new InvalidOperationException("Could not get context from connection context factory.");
            }

            var connectionString = _connectionStringBuilder.InitializeWith(connectionContext.ConnectionString).Build();

            return new SqlConnection(connectionString);
        }
    }
}
