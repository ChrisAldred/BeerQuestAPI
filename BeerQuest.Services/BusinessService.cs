using System;
using System.Threading.Tasks;
using BeerQuest.Models;
using BeerQuest.Providers;

namespace BeerQuest.Services
{
    public class BusinessService : IBusinessService
    {
        private readonly IBusinessProvider _businessProvider;

        public BusinessService(IBusinessProvider businessProvider)
        {
            _businessProvider = businessProvider ?? throw new ArgumentNullException(nameof(businessProvider));
        }

        public Task<Businesses> GetBusinessesAsync(BusinessRequest request)
        {
            return _businessProvider.GetBusinessesAsync(request);
        }
    }
}
