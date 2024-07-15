using MongoDB.Bson;
using SpecialOffers.Domain.Models;
using System.Linq.Expressions;

namespace SpecialOffers.Domain.Interfaces
{
    public interface ISpecialOfferService
    {        
        Task<OperationResultModel> AddOffer(SpecialOfferPostModel offer);
        Task<OperationResultModel> UpdateOffer(SpecialOfferPutModel offer);
        Task<OperationResultModel> FindSync(ObjectId Id);
        Task<OperationResultModel> FindSync(string Id);
        Task<OperationResultModel> FindAsync(Expression<Func<SpecialOfferViewModel, bool>> filter);
        Task<OperationResultModel> FindOffers(HashSet<string> productIds);
        Task<OperationResultModel> FindOffers();
    }
}
