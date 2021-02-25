using System;
using BeerQuest.Providers.Connection;
using Microsoft.Extensions.Configuration;

namespace BeerQuest.API.Infrastructure
{
    public class ConnectionStringResolver : IConnectionStringResolver
    {
        private readonly IConfiguration _configuration;

        public ConnectionStringResolver(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public string Resolve()
        {
            return _configuration.GetConnectionString("BeerQuestDb");
        }
    }
}