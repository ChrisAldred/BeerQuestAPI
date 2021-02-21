using System.Threading.Tasks;
using BeerQuest.Models;

namespace BeerQuest.Services
{
    public interface IBusinessService
    {
        Task<Businesses> GetBusinessesAsync(BusinessRequest request);
    }
}