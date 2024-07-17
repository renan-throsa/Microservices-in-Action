using ShoppingCart.Domain.Entites;
using System.Text.Json.Serialization;

namespace ShoppingCart.Domain.Models
{
    public record CartItemViewModel(
        [property: JsonPropertyName("id")] string ProductCatalogueId,
        [property: JsonPropertyName("name")] string ProductName,
        [property: JsonPropertyName("description")] string Description,
        [property: JsonPropertyName("price")] Money Price)
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
