using MongoDB.Bson;

namespace LoyaltyProgram.Domain.Entities
{
    public record LoyaltyProgramUser(ObjectId Id, string Name, int LoyaltyPoints, LoyaltyProgramSettings Settings);

    public record LoyaltyProgramSettings()
    {
        public LoyaltyProgramSettings(string[] interests) : this()
        {
            Interests = interests;
        }

        public string[] Interests { get; init; } = Array.Empty<string>();
    }
}
