using LoyaltyProgram.Domain.Entities;
using LoyaltyProgram.Domain.Interfaces;

namespace LoyaltyProgram.Data
{
    public class EventRepository : IEventRepository
    {
        private IEnumerable<Event> _database;

        public EventRepository()
        {
            _database = new List<Event>();
        }

        public IEnumerable<Event> GetEvents(long firstEventSequenceNumber, long lastEventSequenceNumber)
        {
            return _database.Where(x => x.SequenceNumber >= firstEventSequenceNumber && x.SequenceNumber <= lastEventSequenceNumber);
        }


        public void Raise(string eventName, object content)
        {
            var next = GetNextSequencyEventNumber();
            var e = new Event(next, DateTime.Now, eventName, content);
            _database = _database.Append(e);
        }

        public long GetNextSequencyEventNumber()
        {
            if (_database.Any()) return _database.Max(x => x.SequenceNumber) + 1;
            return 1;
        }

        public void Add(Event specialOffer)
        {
            _database.Append(specialOffer);
        }
    }
}
