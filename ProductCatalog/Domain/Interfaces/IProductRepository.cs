using MongoDB.Bson;
using MongoDB.Driver.Linq;
using ProductCatalog.Domain.Entities;
using System.Linq.Expressions;

namespace ProductCatalog.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task AddAsync(Product entity);
        Task AddAsync(IEnumerable<Product> entities);
        Task<Product> FindSync(ObjectId key);
        Task<Product> FindSync(string Id);
        Task<IEnumerable<Product>> FindAsync(Expression<Func<Product, bool>> filter);
        Task DeleteAsync(Product entity);
        Task DeleteAsync(string Id);
        Task DeleteAsync(ObjectId Id);
        Task<Product> UpdateAsync(Product entity);
        Task<IEnumerable<Product>> UpdateAsync(IEnumerable<Product> entities);
        IEnumerable<Product> All(Expression<Func<Product, Product>> projection);
        IQueryable<Product> GetQueryable();
    }
}
