using LoyaltyProgram.Domain;

namespace LoyaltyProgram.Data
{
    public interface ILoyaltyProgramUserStore
    {
        LoyaltyProgramUser GetBy(int userId);
        LoyaltyProgramUser Save(LoyaltyProgramUser user);

    }
}
