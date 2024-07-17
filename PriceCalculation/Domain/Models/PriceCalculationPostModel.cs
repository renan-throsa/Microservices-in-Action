using System.Text.Json.Serialization;

namespace PriceCalculation.Domain.Models
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

    public record CartItemViewModel([property: JsonPropertyName("id")]  string ProductCatalogueId, [property: JsonPropertyName("name")] string ProductName, [property: JsonPropertyName("description")] string Description, Money Price)
    {
        public virtual bool Equals(CartItemViewModel? obj)
        {
            return obj != null && ProductCatalogueId.Equals(obj.ProductCatalogueId);
        }

        public override int GetHashCode()
        {
            return ProductCatalogueId.GetHashCode();
        }
    };

    public record Money([property: JsonPropertyName("currency")] string Currency, [property: JsonPropertyName("amount")] float Amount);
}
