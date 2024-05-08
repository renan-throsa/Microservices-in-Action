using LoyaltyProgram.Domain;

namespace LoyaltyProgram.Data
{
    public class LoyaltyProgramUserStore : ILoyaltyProgramUserStore
    {
        private readonly Dictionary<int, LoyaltyProgramUser> _database;

        public LoyaltyProgramUserStore()
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
