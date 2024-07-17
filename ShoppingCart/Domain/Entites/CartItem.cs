using MongoDB.Bson;
using System.Text.Json.Serialization;

namespace ShoppingCart.Domain.Entites
{
    public record CartItem(ObjectId ProductCatalogueId, string ProductName, string Description, Money Price);

    public record Money([property: JsonPropertyName("currency")] string Currency, [property: JsonPropertyName("amount")] decimal Amount);
}
