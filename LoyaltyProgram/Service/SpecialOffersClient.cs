using LoyaltyProgram.Domain.Interfaces;
using LoyaltyProgram.Domain.Models;
using System.Text.Json;

namespace LoyaltyProgram.Service
{

    public class SpecialOffersClient : ISpecialOffersClient
    {
        private readonly HttpClient _client;

        public SpecialOffersClient(HttpClient client)
        {
            _client = client;
        }

        /// <summary>
        /// Get today's offers
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<SpecialOfferViewModel>> GetOffers()
        {
            using var response = await _client.GetAsync($"EventFeed");

            return await ConvertToSpecialOffers(response);
        }

        private async Task<IEnumerable<SpecialOfferViewModel>> ConvertToSpecialOffers(HttpResponseMessage response)
        {
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();

            var option = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = false
            };

            return JsonSerializer.Deserialize<IEnumerable<SpecialOfferViewModel>>(result, option) ?? new List<SpecialOfferViewModel>();

        }
    }
}
