using System.Text.Json.Serialization;

namespace LoyaltyProgram.Domain
{

    public record Event(
        [property: JsonPropertyName("sequenceNumber")] long SequenceNumber,
        [property: JsonPropertyName("occuredAt")] DateTimeOffset OccuredAt,
        [property: JsonPropertyName("name")] string Name,
        [property: JsonPropertyName("content")] object Content);


}
