using LoyaltyProgram.Domain.Entities;
using LoyaltyProgram.Domain.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Linq.Expressions;

namespace LoyaltyProgram.Data
{
    public class LoyaltyProgramRepository : ILoyaltyProgramRepository
    {

        private ApplicationContext Context { get; }

        private IMongoCollection<LoyaltyProgramUser> _collection;
        protected IMongoCollection<LoyaltyProgramUser> Collection
        {
            get { return _collection ?? (_collection = GetOrCreateEntity($"c_{typeof(LoyaltyProgramUser).Name.ToLower()}")); }
        }

        public LoyaltyProgramRepository(ApplicationContext context)
        {
            Context = context;
        }
        

        private IMongoCollection<LoyaltyProgramUser> GetOrCreateEntity(string entity)
        {
            if (Context.DataBase.GetCollection<LoyaltyProgramUser>(entity) == null)
            {
                return CreateEntity(entity);
            }
            return Context.DataBase.GetCollection<LoyaltyProgramUser>(entity);
        }

        private IMongoCollection<LoyaltyProgramUser> CreateEntity(string entity)
        {
            Context.DataBase.CreateCollection(entity);
            return Context.DataBase.GetCollection<LoyaltyProgramUser>(entity);
        }

        public Task AddAsync(LoyaltyProgramUser entity)
        {
            throw new NotImplementedException();
        }

        public Task AddAsync(IEnumerable<LoyaltyProgramUser> entity)
        {
            throw new NotImplementedException();
        }

        public Task<LoyaltyProgramUser> FindSync(ObjectId key)
        {
            throw new NotImplementedException();
        }

        public Task<LoyaltyProgramUser> FindSync(string Id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<LoyaltyProgramUser>> FindAsync(Expression<Func<LoyaltyProgramUser, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(LoyaltyProgramUser entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(string Id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(ObjectId Id)
        {
            throw new NotImplementedException();
        }

        public Task<LoyaltyProgramUser> UpdateAsync(LoyaltyProgramUser entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<LoyaltyProgramUser>> UpdateAsync(IEnumerable<LoyaltyProgramUser> entities)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<LoyaltyProgramUser> All(Expression<Func<LoyaltyProgramUser, LoyaltyProgramUser>> projection)
        {
            throw new NotImplementedException();
        }

        public IMongoQueryable<LoyaltyProgramUser> GetQueryable()
        {
            throw new NotImplementedException();
        }
    }
}
