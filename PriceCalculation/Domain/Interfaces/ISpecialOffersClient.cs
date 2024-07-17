using PriceCalculation.Domain.Models;

namespace PriceCalculation.Domain.Interfaces
{
    public interface ISpecialOffersClient
    {
        public Task<IEnumerable<SpecialOfferViewModel>> GetOffers(IEnumerable<string> productsIds);
    }
}
