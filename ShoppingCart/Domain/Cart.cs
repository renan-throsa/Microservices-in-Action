using MongoDB.Bson;
using ShoppingCart.Data;

namespace ShoppingCart.Domain
{
    public class Cart
    {
        public ObjectId ShoppingCartId { get; }
        
        public ObjectId UserId { get; }

        private readonly HashSet<CartItem> items = new();        

        public IEnumerable<CartItem> Items => items;

        public Cart(ObjectId userId) => UserId = userId;


        public void AddItems(IEnumerable<CartItem> shoppingCartItems, IEventStore eventStore)
        {
            foreach (var item in shoppingCartItems)
                if (items.Add(item)) eventStore.Raise("ShoppingCartItemAdded", new { UserId, item });
        }

        public void RemoveItems(int[] productCatalogueIds, IEventStore eventStore)
        {
            items.RemoveWhere(i => productCatalogueIds.Contains(i.ProductCatalogueId));
        }
    }

    public record CartItem(int ProductCatalogueId, string ProductName, string Description, Money Price)
    {
        public virtual bool Equals(CartItem? obj)
        {
            return obj != null && ProductCatalogueId.Equals(obj.ProductCatalogueId);
        }

        public override int GetHashCode()
        {
            return ProductCatalogueId.GetHashCode();
        }
    }

    public record Money(string Currency, decimal Amount);

}
