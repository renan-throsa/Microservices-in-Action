using ClientGateway.Domain.Interfaces;
using ClientGateway.Domain.Models;
using System.Text.Json;

namespace ClientGateway.Clients
{
    public class PriceCalculatorClient : IPriceCalculatorClient
    {
        private readonly HttpClient client;
        private const string _CONTROLLER = "/PriceCalculator";

        public PriceCalculatorClient(HttpClient client)
        {
            this.client = client;
        }

        public async Task<PriceCalculationViewModel> CarryOut(PriceCalculationPostModel model)
        {
            using StringContent jsonContent = new(JsonSerializer.Serialize(model));

            using var response = await client.PostAsync(_CONTROLLER, jsonContent);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();

            var option = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = false
            };

            return JsonSerializer.Deserialize<PriceCalculationViewModel>(result, option);
        }
    }
}
