
using PriceCalculation.Domain.Interfaces;
using PriceCalculation.Domain.Models;
using System.Text.Json;

namespace PriceCalculation.Service
{

    public class SpecialOffersClient : ISpecialOffersClient
    {
        private readonly HttpClient _client;
        private readonly string getProductPathTemplate = "/SpecialOffers?productId={0}";
        public SpecialOffersClient(HttpClient client)
        {
            _client = client;
        }
        
        /// <summary>
        /// Get special offers by product id
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<SpecialOfferViewModel>> GetOffers(IEnumerable<string> productsIds)
        {
            var productsResource = string.Format(getProductPathTemplate, string.Join("&productId=", productsIds));
            using var response = await _client.GetAsync(productsResource);
            
            return await ConvertToSpecialOffers(response);
        }

        private static async Task<IEnumerable<SpecialOfferViewModel>> ConvertToSpecialOffers(HttpResponseMessage response)
        {
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();

            var option = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = false
            };

            return JsonSerializer.Deserialize<List<SpecialOfferViewModel>>(result, option) ?? new();

        }
    }
}
