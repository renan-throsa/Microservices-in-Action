using MongoDB.Bson;
using SpecialOffers.Domain;
using SpecialOffers.Domain.Interfaces;
using SpecialOffers.Domain.Models;
using System.Linq.Expressions;

namespace SpecialOffers.Service
{
    public class SpecialOfferService : ISpecialOfferService
    {
        public Task<OperationResultModel> AddOffer(SpecialOfferPostModel offer)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResultModel> FindAsync(Expression<Func<SpecialOfferViewModel, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResultModel> FindSync(ObjectId Id)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResultModel> FindSync(string Id)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResultModel> UpdateOffer(SpecialOfferPutModel offer)
        {
            throw new NotImplementedException();
        }
    }
}
