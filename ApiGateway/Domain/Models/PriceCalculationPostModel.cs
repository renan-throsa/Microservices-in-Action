using System.Text.Json.Serialization;

namespace ClientGateway.Domain.Models
{
    public record PriceCalculationPostModel
    {
        [property: JsonPropertyName("id")]
        public string CartId { get; set; }

        [property: JsonPropertyName("userId")]
        public string UserId { get; set; }

        public HashSet<CartItemViewModel> Items { get; set; } = new();

        public IEnumerable<string> ItemsIds => Items.Select(i => i.ProductCatalogueId);

        public CartItemViewModel? GetBy(string productId)
        {
            return Items.FirstOrDefault(i => i.ProductCatalogueId == productId);
        }
    }

}
