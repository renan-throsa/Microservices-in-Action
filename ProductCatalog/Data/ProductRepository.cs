using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using ProductCatalog.Domain.Entities;
using ProductCatalog.Domain.Interfaces;
using System.Linq.Expressions;

namespace ProductCatalog.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationContext _context;
        private IMongoCollection<Product> _entities;

        public ProductRepository(ApplicationContext context)
        {
            _context = context;
            _entities = _context.DataBase.GetCollection<Product>($"c_{typeof(Product).Name.ToLower()}");
        }

        public Task AddAsync(Product product)
        {
            return _entities.InsertOneAsync(product);
        }

        public Task AddAsync(IEnumerable<Product> products)
        {
            return _entities.InsertManyAsync(products);
        }

        public IEnumerable<Product> All(Expression<Func<Product, Product>> projection)
        {
            return _entities.AsQueryable().Select(projection).AsEnumerable();
        }

        public Task DeleteAsync(Product entity)
        {
            return DeleteAsync(entity.Id);
        }

        public Task DeleteAsync(string Id)
        {
            return DeleteAsync(ObjectId.Parse(Id));
        }

        public async Task DeleteAsync(ObjectId Id)
        {
            Product product = await FindSync(Id);
            Product deleteded = product with { Available = false};            
            await UpdateAsync(deleteded);            
        }

        public async Task<IEnumerable<Product>> FindAsync(Expression<Func<Product, bool>> filter)
        {
            var query = await _entities.FindAsync(filter);
            return query.ToEnumerable();
        }

        public async Task<Product> FindSync(ObjectId key)
        {
            var query = await _entities.FindAsync(x => x.Id == key);
            return await query.FirstOrDefaultAsync();            
        }

        public async Task<Product> FindSync(string Id)
        {
            return await FindSync(ObjectId.Parse(Id));
        }

        public IMongoQueryable<Product> GetQueryable()
        {
            return _entities.AsQueryable();
        }

        public async Task<Product> UpdateAsync(Product entity)
        {
            return await _entities.FindOneAndReplaceAsync(x => x.Id == entity.Id, entity);
        }

        public Task<IEnumerable<Product>> UpdateAsync(IEnumerable<Product> entities)
        {
            throw new NotImplementedException();
        }
    }
}
