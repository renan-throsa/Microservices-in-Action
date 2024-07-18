
using System.Text.Json.Serialization;

namespace ClientGateway.Domain.Models
{
    public record SpecialOfferViewModel(
        [property: JsonPropertyName("id")] string Id,        
        [property: JsonPropertyName("occuredAt")] DateTime OccuredAt,
        [property: JsonPropertyName("dueDate")] DateTime DueDate,
        [property: JsonPropertyName("name")] string Name,
        [property: JsonPropertyName("description")] string Description,
        [property: JsonPropertyName("productsIds")] string[] productsIds,
        [property: JsonPropertyName("discount")]  float discount);
}
