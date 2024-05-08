using LoyaltyProgram.Domain;

namespace LoyaltyProgram.Data
{
    public interface IEventStore
    {
        IEnumerable<Event> GetEvents(long firstEventSequenceNumber, long lastEventSequenceNumber);
        void Raise(string eventName, object content);
        void Add(Event specialOffer);
        long GetStartIdFromDatastore();
    }
}
