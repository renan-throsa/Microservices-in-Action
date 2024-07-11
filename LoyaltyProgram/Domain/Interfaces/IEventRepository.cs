using LoyaltyProgram.Domain.Entities;

namespace LoyaltyProgram.Domain.Interfaces
{
    public interface IEventRepository
    {
        IEnumerable<Event> GetEvents(long firstEventSequenceNumber, long lastEventSequenceNumber);
        void Raise(string eventName, object content);
        void Add(Event specialOffer);
        long GetNextSequencyEventNumber();
    }
}
