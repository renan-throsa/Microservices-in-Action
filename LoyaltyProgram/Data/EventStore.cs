

using LoyaltyProgram.Domain;

namespace LoyaltyProgram.Data
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
            return _database.Where(x => x.SequenceNumber >= firstEventSequenceNumber && x.SequenceNumber <= lastEventSequenceNumber);
        }


        public void Raise(string eventName, object content)
        {
            var next = GetStartIdFromDatastore() + 1;
            var e = new Event(next, DateTime.Now, eventName, content);
            _database = _database.Append(e);
        }

        public long GetStartIdFromDatastore()
        {
            if (_database.Any()) return _database.Max(x => x.SequenceNumber);
            return 1;
        }

        public void Add(Event specialOffer)
        {
            _database.Append(specialOffer);
        }
    }
}
