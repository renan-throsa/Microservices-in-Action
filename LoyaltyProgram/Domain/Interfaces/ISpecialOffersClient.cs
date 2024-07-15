using LoyaltyProgram.Domain.Models;

namespace LoyaltyProgram.Domain.Interfaces
{
    public interface ISpecialOffersClient
    {
        public Task<IEnumerable<SpecialOfferViewModel>> GetOffers();
    }
}
