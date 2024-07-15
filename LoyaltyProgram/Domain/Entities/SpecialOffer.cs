using MongoDB.Bson;

namespace LoyaltyProgram.Domain.Entities
{
    public record SpecialOffer(
        ObjectId Id,
        DateTime OccuredAt,
        DateTime DueDate,
        string Name,
        string Description,
        HashSet<string> ProductsIds,
        float discount);

}
