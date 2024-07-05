using ShoppingCart.Domain;

namespace ShoppingCart.Data
{
    public interface IEventStore
    {
        Task<IEnumerable<Event>> GetEvents(long firstEventSequenceNumber, long lastEventSequenceNumber);
        Task Raise(string eventName, object content);
    }
}
