using ShoppingCart.Domain.Entites;
using ShoppingCart.Domain.Interfaces;
using ShoppingCart.Domain.Models;
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
        private static string getProductPathTemplate = "/Product/GetMany?Id={0}";

        public ProductCatalogClient(HttpClient client)
        {
            this.client = client;
        }

        public async Task<IEnumerable<CartItem>> GetShoppingCartItems(string[] productCatalogIds)
        {
            var productsResource = string.Format(getProductPathTemplate, string.Join("&Id=", productCatalogIds));
            using var response = await client.GetAsync(productsResource);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();

            var option = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = false
            };

            return JsonSerializer.Deserialize<List<CartItem>>(result, option) ?? new();
        }      

        
        
    }
}
