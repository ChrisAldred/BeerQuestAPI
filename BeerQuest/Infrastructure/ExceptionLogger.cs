using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeerQuest.Providers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;

namespace BeerQuest.API.Infrastructure
{
    public class ExceptionLogger : IExceptionLogger
    {
        public const string System = "Beer Quest API";
        private readonly IExceptionProvider _exceptionProvider;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ExceptionLogger(IExceptionProvider exceptionProvider, IHttpContextAccessor httpContextAccessor)
        {
            _exceptionProvider = exceptionProvider ?? throw new ArgumentNullException(nameof(exceptionProvider));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public Task LogExceptionAsync(Exception exception)
        {
            var httpContext = _httpContextAccessor.HttpContext;

            return _exceptionProvider.StoreException(
                System,
                Environment.MachineName,
                httpContext.Request.GetEncodedUrl(),
                exception.Message,
                exception.StackTrace
            );
        }
    }
}
