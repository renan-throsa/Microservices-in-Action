using LoyaltyProgram.Domain.Entities;

namespace LoyaltyProgram.Domain.Interfaces
{
    public interface IEventRepository
    {        
        Task AddEvents(IEnumerable<SpecialOffer> offers);
    }
}
