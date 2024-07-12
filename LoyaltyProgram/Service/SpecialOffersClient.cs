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
        
        public async Task<IEnumerable<SpecialOfferViewModel>> GetOffers(long firstEventSequenceNumber, long lastEventSequenceNumber)
        {
            using var response = await _client.GetAsync($"EventFeed?start={firstEventSequenceNumber}&end={lastEventSequenceNumber}");
            
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
