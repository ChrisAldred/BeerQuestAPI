using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeerQuest.API.Infrastructure
{
    public interface IExceptionLogger
    {
        Task LogExceptionAsync(Exception exception);
    }
}
