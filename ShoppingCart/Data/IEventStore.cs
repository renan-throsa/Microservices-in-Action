using ShoppingCart.Domain;

namespace ShoppingCart.Data
{
    public interface IEventStore
    {
        IEnumerable<Event> GetEvents(long firstEventSequenceNumber, long lastEventSequenceNumber);
        void Raise(string eventName, object content);
    }
}
