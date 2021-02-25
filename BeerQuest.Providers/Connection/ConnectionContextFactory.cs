using System;

namespace BeerQuest.Providers.Connection
{
    public class ConnectionContextFactory : IConnectionContextFactory
    {
        private readonly IConnectionStringResolver _connectionStringResolver;

        public ConnectionContextFactory(IConnectionStringResolver connectionStringResolver)
        {
            _connectionStringResolver = connectionStringResolver ??
                                        throw new ArgumentNullException(nameof(connectionStringResolver));
        }

        public ConnectionContext GetCurrentConnectionContext()
        {
            return new ConnectionContext(_connectionStringResolver.Resolve());
        }
    }
}
