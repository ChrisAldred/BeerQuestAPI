using BeerQuest.Providers.Connection;
using Microsoft.Extensions.DependencyInjection;

namespace BeerQuest.Providers
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDatabaseProvider(this IServiceCollection services)
        {
            services.AddSingleton<IConnectionContextFactory, ConnectionContextFactory>();
            services.AddTransient<IConnectionFactory, SqlConnectionFactory>();
            services.AddTransient<IConnectionStringBuilder, ConnectionStringBuilder>();
            services.AddScoped<IDataRepository, DataRepository>();
            services.AddTransient<IExceptionProvider, ExceptionProvider>();
            services.AddTransient<IBusinessProvider, BusinessProvider>();
        }
    }
}
