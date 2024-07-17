using PriceCalculation.Domain;
using PriceCalculation.Domain.Interfaces;
using PriceCalculation.Domain.Models;
using System.Net;

namespace PriceCalculation.Services
{
    public class PriceCalculationService : IPriceCalculationService
    {
        private readonly ISpecialOffersClient _specialOffersClient;

        public PriceCalculationService(ISpecialOffersClient specialOffersClient)
        {
            _specialOffersClient = specialOffersClient;
        }

        public async Task<OperationResultModel> CarryOut(PriceCalculationPostModel model)
        {
            IEnumerable<SpecialOfferViewModel> offers = await _specialOffersClient.GetOffers(model.ItemsIds);

            float totalPrice = 0;

            if (!offers.Any())
            {
                totalPrice = model.Items.Sum(i => i.Price.Amount);
            }
            else
            {

                foreach (var product in model.Items)
                {
                    var o = GetOfferRelatedToProduct(offers, product.ProductCatalogueId);
                    totalPrice += o is not null ? product.Price.Amount - (product.Price.Amount * o.discount) : product.Price.Amount;
                }
            };

            return Response(HttpStatusCode.OK, new PriceCalculationViewModel(totalPrice, model, offers));
        }

        private SpecialOfferViewModel? GetOfferRelatedToProduct(IEnumerable<SpecialOfferViewModel> offers, string productId)
        {
            return offers.FirstOrDefault(o => o.productsIds.Any(p => p == productId));
        }

        private OperationResultModel Response(HttpStatusCode status, object? result = null)
        {
            return
            new OperationResultModel
            {
                Status = status,
                Content = result
            };
        }
    }
}
