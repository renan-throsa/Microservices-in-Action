using LoyaltyProgram.Domain.Entities;
using LoyaltyProgram.Domain.Interfaces;
using MongoDB.Driver;

namespace LoyaltyProgram.Data
{
    public class EventRepository : IEventRepository
    {
        private ApplicationContext Context { get; }

        private IMongoCollection<SpecialOffer> _collection;
        protected IMongoCollection<SpecialOffer> Collection
        {
            get { return _collection ?? (_collection = GetOrCreateEntity($"c_{typeof(SpecialOffer).Name.ToLower()}")); }
        }

        public EventRepository(ApplicationContext context)
        {
            Context = context;
        }


        public Task AddEvents(IEnumerable<SpecialOffer> offers)
        {
            return Collection.InsertManyAsync(offers);
        }

        public long GetNextSequencyEventNumber()
        {
            if (Collection.AsQueryable().Any()) return Collection.AsQueryable().Max(x => x.SequenceNumber) + 1;
            return 1;
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

    }
}
