using SpecialOffers.Domain;

namespace SpecialOffers.Data
{
    public class EventStore : IEventStore
    {
        private readonly ILogger<EventStore> _logger;
        private IEnumerable<Event> _database;

        public EventStore(ILogger<EventStore> logger)
        {
            _logger = logger;
            _database = new List<Event>
{
    new Event(1, DateTimeOffset.Parse("2020-06-16T20:13:53.6678934+00:00"), "SpecialOfferCreated", new { description = "Best deal ever!!!", id = 0 }),
    new Event(2, DateTimeOffset.Parse("2020-06-16T20:14:22.6229836+00:00"), "SpecialOfferCreated", new { description = "Special offer - just for you", id = 1 }),
    new Event(3, DateTimeOffset.Parse("2020-06-16T20:14:39.841415+00:00"), "SpecialOfferCreated", new { description = "Nice deal", id = 2 }),
    new Event(4, DateTimeOffset.Parse("2020-06-16T20:14:47.3420926+00:00"), "SpecialOfferUpdated", new { oldOffer = new { description = "Nice deal", id = 2 }, newOffer = new { description = "Best deal ever - JUST GOT BETTER", id = 0 } }),
    new Event(5, DateTimeOffset.Parse("2020-06-16T20:14:51.8986625+00:00"), "SpecialOfferRemoved", new { offer = new { description = "Special offer - just for you", id = 1 } })
};

        }

        public IEnumerable<Event> GetEvents(long firstEventSequenceNumber, long lastEventSequenceNumber)
        {
            _logger.LogInformation($"Sending events from {firstEventSequenceNumber} to {lastEventSequenceNumber}");
            return _database.Where(x => x.SequenceNumber >= firstEventSequenceNumber && x.SequenceNumber <= lastEventSequenceNumber);
        }

        public void Raise(string eventName, object content)
        {
            var max = _database.Max(x => x.SequenceNumber);
            var e = new Event(max + 1, DateTime.Now, eventName, content);
            _database = _database.Append(e);
        }
    }
}
