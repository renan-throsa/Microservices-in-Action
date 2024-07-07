using MongoDB.Driver;
using ShoppingCart.Domain.Entites;
using ShoppingCart.Domain.Interfaces;
using System.Text.Json;

namespace ShoppingCart.Data
{
    public class EventRepository : IEventRepository
    {
        private ApplicationContext Context { get; }

        private IMongoCollection<Event> _collection;
        protected IMongoCollection<Event> Collection
        {
            get { return _collection ?? (_collection = GetOrCreateEntity($"c_{typeof(Event).Name.ToLower()}")); }
        }

        public EventRepository(ApplicationContext context)
        {
            Context = context;
        }

        public async Task<IEnumerable<Event>> GetEvents(long firstEventSequenceNumber, long lastEventSequenceNumber)
        {
            var query = await Collection.FindAsync(x => x.SequenceNumber >= firstEventSequenceNumber && x.SequenceNumber <= lastEventSequenceNumber);
            return query.ToEnumerable();
        }

        public Task AddEvent(string eventName, object content)
        {
            var jsonContent = JsonSerializer.Serialize(content);
            var max = Collection.AsQueryable().Any() ? Collection.AsQueryable().Max(x => x.SequenceNumber) : 1;
            var e = new Event(max + 1, DateTime.Now, eventName, jsonContent);
            return Collection.InsertOneAsync(e);
        }


        private IMongoCollection<Event> GetOrCreateEntity(string entity)
        {
            if (Context.DataBase.GetCollection<Event>(entity) == null)
            {
                return CreateEntity(entity);
            }
            return Context.DataBase.GetCollection<Event>(entity);
        }

        private IMongoCollection<Event> CreateEntity(string entity)
        {
            Context.DataBase.CreateCollection(entity);
            return Context.DataBase.GetCollection<Event>(entity);
        }
    }
}
