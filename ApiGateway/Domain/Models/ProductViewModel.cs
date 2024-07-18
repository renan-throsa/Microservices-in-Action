using System.Text.Json.Serialization;

namespace ClientGateway.Domain.Models
{
    public record ProductViewModel(
        [property: JsonPropertyName("id")] string Id,
        [property: JsonPropertyName("name")] string Name,
        [property: JsonPropertyName("code")] string Code,
        [property: JsonPropertyName("description")] string Description,
        [property: JsonPropertyName("price")] MoneyModel Price,
        [property: JsonPropertyName("available")] bool Available);
}
