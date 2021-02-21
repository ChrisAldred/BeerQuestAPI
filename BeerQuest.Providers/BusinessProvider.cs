using System;
using System.Threading.Tasks;
using BeerQuest.Models;

namespace BeerQuest.Providers
{
    public class BusinessProvider : IBusinessProvider
    {
        private readonly IDataRepository _dataRepository;

        public BusinessProvider(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository ?? throw new ArgumentNullException(nameof(dataRepository));
        }

        public async Task<Businesses> GetBusinessesAsync(BusinessRequest request)
        {
            return new Businesses
            {
                Offset = request.Offset,
                Limit = request.Limit,
                BusinessCollection = await _dataRepository.ExecuteStoredProcedureAsync<Business>(
                    "usp_GetBusinesses",
                    new
                    {
                        Name = request.Name,
                        Category = request.Category,
                        Tag = request.Tag,
                        Offset = request.Offset,
                        Limit = request.Limit
                    })
            };
        }
    }
}