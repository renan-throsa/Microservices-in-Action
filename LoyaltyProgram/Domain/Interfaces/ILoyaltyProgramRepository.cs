using LoyaltyProgram.Domain.Entities;
using MongoDB.Bson;
using MongoDB.Driver.Linq;
using System.Linq.Expressions;

namespace LoyaltyProgram.Domain.Interfaces
{
    public interface ILoyaltyProgramRepository
    {       

        Task AddAsync(LoyaltyProgramUser entity);
        Task AddAsync(IEnumerable<LoyaltyProgramUser> entity);
        Task<LoyaltyProgramUser> FindSync(ObjectId key);
        Task<LoyaltyProgramUser> FindSync(string Id);
        Task<IEnumerable<LoyaltyProgramUser>> FindAsync(Expression<Func<LoyaltyProgramUser, bool>> filter);
        Task DeleteAsync(LoyaltyProgramUser entity);
        Task DeleteAsync(string Id);
        Task DeleteAsync(ObjectId Id);
        Task<LoyaltyProgramUser> UpdateAsync(LoyaltyProgramUser entity);
        Task<IEnumerable<LoyaltyProgramUser>> UpdateAsync(IEnumerable<LoyaltyProgramUser> entities);
        IEnumerable<LoyaltyProgramUser> All(Expression<Func<LoyaltyProgramUser, LoyaltyProgramUser>> projection);
        IMongoQueryable<LoyaltyProgramUser> GetQueryable();

    }
}
