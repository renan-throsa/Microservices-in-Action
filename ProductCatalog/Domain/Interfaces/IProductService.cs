using MongoDB.Bson;
using ProductCatalog.Domain.Models;
using System.Linq.Expressions;

namespace ProductCatalog.Domain.Interfaces
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
