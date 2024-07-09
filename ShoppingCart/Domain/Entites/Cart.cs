using MongoDB.Bson;
using ShoppingCart.Domain.Interfaces;

namespace ShoppingCart.Domain.Entites
{
    public class Cart
    {
        public ObjectId Id { get; set; }

        public ObjectId UserId { get; set; }

        public HashSet<CartItem> Items { get; set; }

        public Cart(ObjectId userId)
        {
            UserId = userId;
            Items = new HashSet<CartItem>();
        }

        public async Task AddItems(IEnumerable<CartItem> shoppingCartItems, IEventRepository eventStore)
        {
            foreach (var item in shoppingCartItems)
                if (Items.Add(item)) await eventStore.AddEvent("ShoppingCartItemAdded", UserId, new ObjectId(item.ProductCatalogueId));
        }

        public void RemoveItems(string[] productCatalogueIds, IEventRepository eventStore)
        {
            Items.RemoveWhere(i => productCatalogueIds.Contains(i.ProductCatalogueId));
        }
    }


}
