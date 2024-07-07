using MongoDB.Bson;

namespace ShoppingCart.Domain
{
    public abstract class BaseEntity
    {
        protected ObjectId Id { get; set; }
    }
}
