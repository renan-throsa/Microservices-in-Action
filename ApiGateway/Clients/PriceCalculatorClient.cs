using ClientGateway.Domain.Interfaces;
using ClientGateway.Domain.Models;
using System.Text;
using System.Text.Json;

namespace ClientGateway.Clients
{
    public class PriceCalculatorClient : IPriceCalculatorClient
    {
        private readonly HttpClient client;
        private const string _CONTROLLER = "/PriceCalculation";

        public PriceCalculatorClient(HttpClient client)
        {
            this.client = client;
        }

        public async Task<PriceCalculationViewModel> CarryOut(PriceCalculationPostModel model)
        {
            using StringContent jsonContent = new(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");

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
