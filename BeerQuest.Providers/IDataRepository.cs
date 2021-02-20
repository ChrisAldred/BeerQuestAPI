using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeerQuest.Providers
{
    public interface IDataRepository
    {
        Task<IReadOnlyList<T>> ExecuteStoredProcedureAsync<T>(string procedureName, object param);

        Task ExecuteStoredProcedureAsync(string procedureName, object param);
    }
}