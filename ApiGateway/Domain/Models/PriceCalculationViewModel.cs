using System.Text.Json.Serialization;

namespace ClientGateway.Domain.Models
{
    public record PriceCalculationViewModel(
        [property: JsonPropertyName("totalPrice")] float TotalPrice,
        [property: JsonPropertyName("cart")] PriceCalculationPostModel cart,
        [property: JsonPropertyName("offers")] IEnumerable<SpecialOfferViewModel> offers
        );
}
