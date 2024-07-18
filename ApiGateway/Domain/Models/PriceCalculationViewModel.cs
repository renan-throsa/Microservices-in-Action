﻿namespace ClientGateway.Domain.Models
{
    public record PriceCalculationViewModel(float TotalPrice, PriceCalculationPostModel cart, IEnumerable<SpecialOfferViewModel>? offers = null);
}
