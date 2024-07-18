using ClientGateway.Domain.Interfaces;
using ClientGateway.Domain.Models;
using System.Text.Json;

namespace ClientGateway.Clients
{
    public class ShopingCartClient : IShopingCartClient
    {
        private readonly HttpClient client;
        private const string _CONTROLLER = "/ShoppingCart";

        public ShopingCartClient(HttpClient client)
        {
            this.client = client;
        }

        public async Task<CartViewModel> Get(string userId)
        {
            using var response = await client.GetAsync(_CONTROLLER + "/" + userId);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();

            var option = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = false
            };

            return JsonSerializer.Deserialize<CartViewModel>(result, option);
        }

        public async Task<CartViewModel> DeleteItems(CartPostModel model)
        {
            using StringContent jsonContent = new(JsonSerializer.Serialize(model));

            using var response = await client.PutAsync(_CONTROLLER + "/items", jsonContent);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();

            var option = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = false
            };

            return JsonSerializer.Deserialize<CartViewModel>(result, option);
        }

        public async Task<CartViewModel> PostItems(CartPostModel model)
        {
            using StringContent jsonContent = new(JsonSerializer.Serialize(model));

            using var response = await client.PostAsync(_CONTROLLER + "/items", jsonContent);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();

            var option = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = false
            };

            return JsonSerializer.Deserialize<CartViewModel>(result, option);
        }
    }
}
