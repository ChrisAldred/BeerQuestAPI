using System;
using System.Threading.Tasks;

namespace BeerQuest.Providers
{
    public class ExceptionProvider : IExceptionProvider
    {
        private readonly IDataRepository _dataRepository;

        public ExceptionProvider(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository ?? throw new ArgumentNullException(nameof(dataRepository));
        }

        public async Task StoreException(string system, string machineName, string requestUrl, string message, string stackTrace)
        {
            await _dataRepository.ExecuteStoredProcedureAsync("[dbo].[usp_InsertException]",
                new
                {
                    System = system,
                    MachineName = machineName,
                    Request = requestUrl,
                    Message = message,
                    StackTrace = stackTrace
                });
        }
    }
}