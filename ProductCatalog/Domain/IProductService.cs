using MongoDB.Bson;
using ProductCatalog.Services;
using System.Linq.Expressions;

namespace ProductCatalog.Domain
{
    public interface IProductService
    {

        Task<OperationResultModel> FindSync(ObjectId Id);
        Task<OperationResultModel> FindSync(string Id);
        Task<OperationResultModel> FindSync(string[] Ids);
        Task<OperationResultModel> FindAsync(Expression<Func<ProductViewModel, bool>> filter);
        OperationResultModel All();
    }
}
