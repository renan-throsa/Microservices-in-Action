using MongoDB.Bson;

namespace ShoppingCart.Domain.Entities
{
    public abstract class Entity
    {
        public ObjectId Id { get; set; }
    }
}
