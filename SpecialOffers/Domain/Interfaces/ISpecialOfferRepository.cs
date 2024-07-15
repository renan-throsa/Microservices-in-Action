using SpecialOffers.Domain.Entities;

namespace SpecialOffers.Domain.Interfaces
{
    public interface ISpecialOfferRepository
    {
        Task<IEnumerable<SpecialOffer>> FindOffers();
        Task<IEnumerable<SpecialOffer>> FindOffers(HashSet<string> productIds);
        Task<SpecialOffer> FindSync(string id);
        Task AddSync(SpecialOffer offer);
        Task UpdateAsync(SpecialOffer offer);
        IQueryable<SpecialOffer> GetQueryable();

    }
}
