using System.Text.Json.Serialization;

namespace ClientGateway.Domain.Models
{
    public record CartViewModel
    (
        [property: JsonPropertyName("id")] string Id,
        [property: JsonPropertyName("userId")] string UserId,
        [property: JsonPropertyName("items")] HashSet<CartItemViewModel> items
    );
}
