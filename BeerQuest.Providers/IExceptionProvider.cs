using System.Threading.Tasks;

namespace BeerQuest.Providers
{
    public interface IExceptionProvider
    {
        Task StoreException(string system, string machineName, string requestUrl, string message, string stackTrace);
    }
}