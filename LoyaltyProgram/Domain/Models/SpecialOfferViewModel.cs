
using System.Text.Json.Serialization;

namespace LoyaltyProgram.Domain.Models
{
    public record SpecialOfferViewModel(
        [property: JsonPropertyName("id")] string Id,
        [property: JsonPropertyName("sequenceNumber")] long SequenceNumber,
        [property: JsonPropertyName("occuredAt")] DateTime OccuredAt,
        [property: JsonPropertyName("name")] string Name,
        [property: JsonPropertyName("description")] string Description,
        [property: JsonPropertyName("oldOfferId")] string? OldOfferId = null);
}
