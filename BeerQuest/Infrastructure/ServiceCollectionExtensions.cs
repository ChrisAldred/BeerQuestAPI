using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeerQuest.Providers.Connection;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace BeerQuest.API.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddConnectionContextResolvers(this IServiceCollection services)
        {
            services.AddSingleton<IConnectionStringResolver, ConnectionStringResolver>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            return services;
        }
    }
}
