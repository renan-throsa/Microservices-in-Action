using ShoppingCart.Domain;

namespace ShoppingCart.Data
{
    public interface IShoppingCartStore
    {
        Task<Cart> GetBy(string userId);
        Task Save(Cart shoppingCart);
    }
}
