using MongoDB.Bson;
using ProductCatalog.Services;
using System.Linq.Expressions;

namespace ProductCatalog.Domain
{
    public interface IProductService
    {

        Task<Response> FindSync(ObjectId Id);
        Task<Response> FindSync(string Id);
        Task<Response> FindSync(string[] Ids);
        Task<Response> FindAsync(Expression<Func<ProductViewModel, bool>> filter);
        Response All();
    }
}
