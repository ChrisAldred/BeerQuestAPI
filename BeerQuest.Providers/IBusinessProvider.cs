using System.Threading.Tasks;
using BeerQuest.Models;

namespace BeerQuest.Providers
{
    public interface IBusinessProvider
    {
        Task<Businesses> GetBusinessesAsync(BusinessRequest request);
    }
}