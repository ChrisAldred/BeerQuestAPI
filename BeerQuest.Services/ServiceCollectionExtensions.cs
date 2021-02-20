using BeerQuest.Providers;
using Microsoft.Extensions.DependencyInjection;

namespace BeerQuest.Services
{
    public static class ServiceCollectionExtensions
    {
        public static void AddBeerQuestServices(this IServiceCollection services)
        {
            services.AddDatabaseProvider();
        }
    }
}
