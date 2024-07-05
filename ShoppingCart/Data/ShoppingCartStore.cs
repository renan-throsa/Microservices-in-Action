using MongoDB.Bson;
using MongoDB.Driver;
using ShoppingCart.Domain;

namespace ShoppingCart.Data
{
    public class ShoppingCartStore : IShoppingCartStore
    {

        private ApplicationContext Context { get; }

        private IMongoCollection<Cart> _collection;
        protected IMongoCollection<Cart> Collection
        {
            get { return _collection ?? (_collection = GetOrCreateEntity<Cart>($"c_{typeof(Cart).Name.ToLower()}")); }
        }

        public ShoppingCartStore(ApplicationContext context)
        {
            Context = context;
        }

        public async Task<Cart> GetBy(string userId)
        {
            return await GetBy(ObjectId.Parse(userId));
        }

        public async Task<Cart> GetBy(ObjectId userId)
        {
            var query = await Collection.FindAsync(x => x.UserId == userId);
            return await query.FirstOrDefaultAsync();
        }


        public Task Save(Cart shoppingCart)
        {
            return Collection.InsertOneAsync(shoppingCart);
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
