using MongoDB.Bson;

namespace ShoppingCart.Domain.Entites
{
    public abstract class Entity
    {
        public ObjectId Id { get; set; }
    }
}
