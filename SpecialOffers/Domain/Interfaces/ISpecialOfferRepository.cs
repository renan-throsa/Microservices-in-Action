using SpecialOffers.Domain.Entities;

namespace SpecialOffers.Domain.Interfaces
{
    public interface ISpecialOfferRepository
    {
        Task<IEnumerable<SpecialOffer>> GetOffers(long firstEventSequenceNumber, long lastEventSequenceNumber);
        Task<SpecialOffer> FindSync(string id);
        Task AddSync(string offerName, string description);
        Task UpdateAsync(SpecialOffer offer);
    }
}
