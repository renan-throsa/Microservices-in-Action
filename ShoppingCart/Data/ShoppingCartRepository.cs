using MongoDB.Bson;
using MongoDB.Driver;
using ShoppingCart.Domain.Entities;
using ShoppingCart.Domain.Interfaces;

namespace ShoppingCart.Data
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {

        private ApplicationContext Context { get; }

        private IMongoCollection<Cart> _collection;
        protected IMongoCollection<Cart> Collection
        {
            get { return _collection ?? (_collection = GetOrCreateEntity($"c_{typeof(Cart).Name.ToLower()}")); }
        }

        public ShoppingCartRepository(ApplicationContext context)
        {
            Context = context;
        }

        public async Task<Cart> FindSync(string userId)
        {
            return await FindSync(ObjectId.Parse(userId));
        }

        private async Task<Cart> FindSync(ObjectId userId)
        {
            var query = await Collection.FindAsync(x => x.UserId == userId);
            return await query.FirstOrDefaultAsync();
        }


        public Task AddSync(Cart shoppingCart)
        {
            return Collection.InsertOneAsync(shoppingCart);
        }

        public async Task<Cart> UpdateSync(Cart shoppingCart)
        {
            return await Collection.FindOneAndReplaceAsync(x => x.UserId == shoppingCart.UserId, shoppingCart);
        }

        public IEnumerable<Cart> FindBy(string productId)
        {
            return Collection
                .AsQueryable()
                .Where(x => x.Items.Select(i => i.ProductCatalogueId).Any(id => id == new ObjectId(productId)))
                .AsEnumerable();
        }

        private IMongoCollection<Cart> GetOrCreateEntity(string entity)
        {
            if (Context.DataBase.GetCollection<Cart>(entity) == null)
            {
                return CreateEntity(entity);
            }
            return Context.DataBase.GetCollection<Cart>(entity);
        }

        private IMongoCollection<Cart> CreateEntity(string entity)
        {
            Context.DataBase.CreateCollection(entity);
            return Context.DataBase.GetCollection<Cart>(entity);
        }


    }
}
