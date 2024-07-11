using LoyaltyProgram.Domain.Entities;
using LoyaltyProgram.Domain.Interfaces;

namespace LoyaltyProgram.Data
{
    public class LoyaltyProgramRepository : ILoyaltyProgramRepository
    {
        private readonly Dictionary<int, LoyaltyProgramUser> _database;

        public LoyaltyProgramRepository()
        {
            _database = new Dictionary<int, LoyaltyProgramUser>();
        }
        public LoyaltyProgramUser GetBy(int userId)
        {
            return _database[userId];
        }

        public LoyaltyProgramUser Save(LoyaltyProgramUser user)
        {
            _database[user.Id] = user;
            return user;
        }
        
    }
}
