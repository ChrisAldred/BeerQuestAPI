using System;

namespace BeerQuest.Providers.Connection
{
    public class ConnectionContext
    {
        public ConnectionContext(string connectionString)
        {
            ConnectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public string ConnectionString { get; }
    }
}
