using ShoppingCart.Domain;

namespace ShoppingCart.Data
{
    public class EventStore : IEventStore
    {
        private IEnumerable<Event> _database;

        public EventStore()
        {
            _database = new List<Event>();
        }

        public IEnumerable<Event> GetEvents(long firstEventSequenceNumber, long lastEventSequenceNumber)
        {
            return _database.Where(x=> x.SequenceNumber >= firstEventSequenceNumber && x.SequenceNumber <= lastEventSequenceNumber);
        }

        public void Raise(string eventName, object content)
        {
            var max = _database.Max(x => x.SequenceNumber);
            var e = new Event(max + 1, DateTime.Now, eventName, content);
            _database = _database.Append(e);
        }
    }
}
