using LoyaltyProgram.Domain.Entities;

namespace LoyaltyProgram.Domain.Interfaces
{
    public interface ILoyaltyProgramRepository
    {
        LoyaltyProgramUser GetBy(int userId);
        LoyaltyProgramUser Save(LoyaltyProgramUser user);

    }
}
