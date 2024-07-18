
using System.Text.Json.Serialization;

namespace ClientGateway.Domain.Models
{
    public record CartItemViewModel(
        [property: JsonPropertyName("id")] string ProductCatalogueId,
        [property: JsonPropertyName("quantity")] int Quantity,
        [property: JsonPropertyName("name")] string ProductName,
        [property: JsonPropertyName("description")] string Description,
        [property: JsonPropertyName("price")] MoneyModel Price)
    {
        public virtual bool Equals(CartItemViewModel? obj)
        {
            return obj != null && ProductCatalogueId.Equals(obj.ProductCatalogueId);
        }

        public override int GetHashCode()
        {
            return ProductCatalogueId.GetHashCode();
        }
    }


}
