using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace BeerQuest.API.Infrastructure
{
    public class ErrorLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorLoggingMiddleware(RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public async Task InvokeAsync(HttpContext httpContext, IExceptionLogger exceptionLogger)
        {
            if (exceptionLogger == null) throw new ArgumentNullException(nameof(exceptionLogger));

            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await exceptionLogger.LogExceptionAsync(ex);
                throw;
            }
        }
    }
}
