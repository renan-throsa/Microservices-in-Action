using ShoppingCart.Domain.Interfaces;
using ShoppingCart.Domain.Models;
using System.Text;
using System.Text.Json;

namespace ShoppingCart.Service
{
    /// <summary>
    /// Using typed clients to encapsulate HTTP calls.
    /// A common pattern when you need to interact with an API is to encapsulate the mechanics of that interaction into a separate service. 
    /// IHttpClientFactory supports typed clients. A typed client is a class that accepts a configured HttpClient in its constructor. 
    /// </summary>
    public class ProductCatalogClient : IProductCatalogClient
    {
        private readonly HttpClient client;
        private const string _CONTROLLER = "/Product";

        public ProductCatalogClient(HttpClient client)
        {
            this.client = client;
        }

        public async Task<IEnumerable<CartItemViewModel>> Query(IEnumerable<string> productCatalogIds)
        {
            using StringContent jsonContent = new(JsonSerializer.Serialize(productCatalogIds), Encoding.UTF8, "application/json");

            using var response = await client.PostAsync(_CONTROLLER + nameof(Query), jsonContent);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();

            var option = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = false
            };

            return JsonSerializer.Deserialize<List<CartItemViewModel>>(result, option) ?? new();
        }

        public async Task<CartItemViewModel> Find(string productCatalogId)
        {
            using var response = await client.GetAsync(_CONTROLLER + "/" + productCatalogId);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();

            var option = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = false
            };

            return JsonSerializer.Deserialize<CartItemViewModel>(result, option);
        }



    }
}
