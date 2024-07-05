using MongoDB.Driver;
using ShoppingCart.Domain;
using System.Text.Json;

namespace ShoppingCart.Data
{
    public class EventStore : IEventStore
    {
        private ApplicationContext Context { get; }

        private IMongoCollection<Event> _collection;
        protected IMongoCollection<Event> Collection
        {
            get { return _collection ?? (_collection = GetOrCreateEntity<Event>($"c_{typeof(Event).Name.ToLower()}")); }
        }


        public EventStore(ApplicationContext context)
        {
            Context = context;
        }

        public async Task<IEnumerable<Event>> GetEvents(long firstEventSequenceNumber, long lastEventSequenceNumber)
        {
            var query = await Collection.FindAsync(x => x.SequenceNumber >= firstEventSequenceNumber && x.SequenceNumber <= lastEventSequenceNumber);
            return query.ToEnumerable();
        }

        public Task Raise(string eventName, object content)
        {
            var jsonContent = JsonSerializer.Serialize(content);
            var max = Collection.AsQueryable().Max(x => x.SequenceNumber);
            var e = new Event(max + 1, DateTime.Now, eventName, jsonContent);
            return Collection.InsertOneAsync(e);
        }


        private IMongoCollection<TEntity> GetOrCreateEntity<TEntity>(string entity)
        {
            if (Context.DataBase.GetCollection<TEntity>(entity) == null)
            {
                return CreateEntity<TEntity>(entity);
            }
            return Context.DataBase.GetCollection<TEntity>(entity);
        }

        private IMongoCollection<TEntity> CreateEntity<TEntity>(string entity)
        {
            Context.DataBase.CreateCollection(entity);
            return Context.DataBase.GetCollection<TEntity>(entity);
        }
    }
}
