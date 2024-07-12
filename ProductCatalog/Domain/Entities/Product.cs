using MongoDB.Bson;

namespace ProductCatalog.Domain.Entities
{
    public record Product(
        ObjectId Id,
        string Name,
        string Code,
        string Description,
        Money Price,
        bool Available = true);
}
