using System.Text.Json.Serialization;

namespace ClientGateway.Domain.Models
{
    public record MoneyModel([property: JsonPropertyName("currency")] string Currency, [property: JsonPropertyName("amount")] float Amount);
}
