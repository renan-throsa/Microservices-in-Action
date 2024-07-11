using SpecialOffers.Domain.Entities;

namespace SpecialOffers.Domain.Interfaces
{
    public interface ISpecialOfferRepository
    {
        Task<IEnumerable<SpecialOffer>> GetOffers(long firstEventSequenceNumber, long lastEventSequenceNumber);
        Task AddOffer(string offerName, string description);
        Task UpdateOffer(SpecialOffer offer);
    }
}
