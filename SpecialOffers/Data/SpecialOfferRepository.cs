using MongoDB.Bson;
using MongoDB.Driver;
using SpecialOffers.Domain.Entities;
using SpecialOffers.Domain.Interfaces;
namespace SpecialOffers.Data
{
    public class SpecialOfferRepository : ISpecialOfferRepository
    {
        private ApplicationContext Context { get; }

        private IMongoCollection<SpecialOffer> _collection;
        protected IMongoCollection<SpecialOffer> Collection
        {
            get { return _collection ?? (_collection = GetOrCreateEntity($"c_{typeof(SpecialOffer).Name.ToLower()}")); }
        }

        public SpecialOfferRepository(ApplicationContext context)
        {
            Context = context;
        }


        public async Task<IEnumerable<SpecialOffer>> GetOffers(long firstEventSequenceNumber, long lastEventSequenceNumber)
        {
            var query = await Collection.FindAsync(x => x.SequenceNumber >= firstEventSequenceNumber && x.SequenceNumber <= lastEventSequenceNumber);
            return query.ToEnumerable();
        }

        public Task AddOffer(string eventName, string description)
        {
            var next = GetNextSequencyEventNumber();
            var e = new SpecialOffer(ObjectId.Empty, next, DateTime.Now, eventName, description);
            return Collection.InsertOneAsync(e);
        }

        public async Task UpdateOffer(SpecialOffer @event)
        {
            await Collection.FindOneAndReplaceAsync(x => x.Id == @event.Id, @event);
        }

        private IMongoCollection<SpecialOffer> GetOrCreateEntity(string entity)
        {
            if (Context.DataBase.GetCollection<SpecialOffer>(entity) == null)
            {
                return CreateEntity(entity);
            }
            return Context.DataBase.GetCollection<SpecialOffer>(entity);
        }

        private IMongoCollection<SpecialOffer> CreateEntity(string entity)
        {
            Context.DataBase.CreateCollection(entity);
            return Context.DataBase.GetCollection<SpecialOffer>(entity);
        }

        private long GetNextSequencyEventNumber()
        {
            if (Collection.AsQueryable().Any()) return Collection.AsQueryable().Max(x => x.SequenceNumber) + 1;
            return 1;
        }
    }
}
