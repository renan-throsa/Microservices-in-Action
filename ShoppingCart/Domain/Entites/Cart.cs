using MongoDB.Bson;
using ShoppingCart.Domain.Interfaces;
using System.Text.Json.Serialization;

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
                if (Items.Add(item)) await eventStore.AddEvent("ShoppingCartItemAdded", UserId, item.ProductCatalogueId);
        }

        public void RemoveItems(IEnumerable<string> productCatalogueIds)
        {
            Items.RemoveWhere(i => productCatalogueIds.Contains(i.ProductCatalogueId.ToString()));
        }
    }

    public record CartItem(ObjectId ProductCatalogueId, int Quantity, string ProductName, string Description, Money Price);

    public record Money([property: JsonPropertyName("currency")] string Currency, [property: JsonPropertyName("amount")] decimal Amount);

}
