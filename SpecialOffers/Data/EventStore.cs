using SpecialOffers.Domain;
using System.Text.Json;

namespace SpecialOffers.Data
{
    public class EventStore : IEventStore
    {
        private IEnumerable<Event> _database;

        public EventStore()
        {
            _database = JsonSerializer.Deserialize<Event[]>(@"[
  {
    ""sequenceNumber"": 1,
    ""occuredAt"": ""2020-06-16T20:13:53.6678934+00:00"",
    ""name"": ""SpecialOfferCreated"",
    ""content"": {
      ""description"": ""Best deal ever!!!"",
      ""id"": 0
    }
  },
  {
    ""sequenceNumber"": 2,
    ""occuredAt"": ""2020-06-16T20:14:22.6229836+00:00"",
    ""name"": ""SpecialOfferCreated"",
    ""content"": {
      ""description"": ""Special offer - just for you"",
      ""id"": 1
    }
  },
  {
    ""sequenceNumber"": 3,
    ""occuredAt"": ""2020-06-16T20:14:39.841415+00:00"",
    ""name"": ""SpecialOfferCreated"",
    ""content"": {
      ""description"": ""Nice deal"",
      ""id"": 2
    }
  },
  {
    ""sequenceNumber"": 4,
    ""occuredAt"": ""2020-06-16T20:14:47.3420926+00:00"",
    ""name"": ""SpecialOfferUpdated"",
    ""content"": {
      ""oldOffer"": {
        ""description"": ""Nice deal"",
        ""id"": 2
      },
      ""newOffer"": {
        ""description"": ""Best deal ever - JUST GOT BETTER"",
        ""id"": 0
      }
    }
  },
  {
    ""sequenceNumber"": 5,
    ""occuredAt"": ""2020-06-16T20:14:51.8986625+00:00"",
    ""name"": ""SpecialOfferRemoved"",
    ""content"": {
      ""offer"": {
        ""description"": ""Special offer - just for you"",
        ""id"": 1
      }
    }
  }
]");
        }

        public IEnumerable<Event> GetEvents(long firstEventSequenceNumber, long lastEventSequenceNumber)
        {
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
