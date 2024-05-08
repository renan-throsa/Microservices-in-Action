using ShoppingCart.Data;

namespace ShoppingCart.Domain
{
    public class ShoppingCart
    {
        private readonly HashSet<ShoppingCartItem> items = new();

        public int UserId { get; }
        public IEnumerable<ShoppingCartItem> Items => items;

        public ShoppingCart(int userId) => UserId = userId;

        public void AddItems(IEnumerable<ShoppingCartItem> shoppingCartItems, IEventStore eventStore)
        {
            foreach (var item in shoppingCartItems)
                if (items.Add(item)) eventStore.Raise("ShoppingCartItemAdded", new { UserId, item });
        }

        public void RemoveItems(int[] productCatalogueIds, IEventStore eventStore)
        {
            items.RemoveWhere(i => productCatalogueIds.Contains(i.ProductCatalogueId));
        }
    }

    public record ShoppingCartItem(int ProductCatalogueId, string ProductName, string Description, Money Price)
    {
        public virtual bool Equals(ShoppingCartItem? obj)
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
