using MongoDB.Bson;
using ShoppingCart.Domain.Interfaces;

namespace ShoppingCart.Domain.Entites
{
    public class Cart
    {
        public ObjectId Id { get; set; }

        public ObjectId UserId { get; set; }

        private readonly HashSet<CartItem> _items;

        public IEnumerable<CartItem> Items => _items;

        public Cart()
        {
            _items = new HashSet<CartItem>();
        }

        public Cart(ObjectId userId)
        {
            _items = new HashSet<CartItem>();
            UserId = userId;
        }


        public void AddItems(IEnumerable<CartItem> shoppingCartItems, IEventRepository eventStore)
        {
            foreach (var item in shoppingCartItems)
                if (_items.Add(item)) eventStore.AddEvent("ShoppingCartItemAdded", new { UserId, item });
        }

        public void RemoveItems(string[] productCatalogueIds, IEventRepository eventStore)
        {
            _items.RemoveWhere(i => productCatalogueIds.Contains(i.ProductCatalogueId));
        }
    }


}
