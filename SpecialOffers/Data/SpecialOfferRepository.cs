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

        public async Task<IEnumerable<SpecialOffer>> FindOffers()
        {
            var query = await Collection.FindAsync(x => x.OccuredAt == DateTime.Today);
            return await query.ToListAsync();
        }

        public async Task<SpecialOffer> FindSync(string id)
        {
            return await FindSync(ObjectId.Parse(id));
        }

        private async Task<SpecialOffer> FindSync(ObjectId Id)
        {
            var query = await Collection.FindAsync(x => x.Id == Id);
            return await query.FirstOrDefaultAsync();
        }

        public Task AddSync(SpecialOffer offer)
        {
            return Collection.InsertOneAsync(offer);
        }

        public async Task UpdateAsync(SpecialOffer @event)
        {
            await Collection.FindOneAndReplaceAsync(x => x.Id == @event.Id, @event);
        }

        public IQueryable<SpecialOffer> GetQueryable()
        {
            return Collection.AsQueryable();
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

        public async Task<IEnumerable<SpecialOffer>> FindOffers(HashSet<string> productIds)
        {
            var query = await Collection.FindAsync(so => so.DueDate >= DateTime.Today && so.ProductsIds.Intersect(productIds).Any());

            return await query.ToListAsync();
        }
    }
}
