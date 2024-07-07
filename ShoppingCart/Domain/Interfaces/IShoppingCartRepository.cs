using ShoppingCart.Domain.Entites;

namespace ShoppingCart.Domain.Interfaces
{
    public interface IShoppingCartRepository
    {
        Task<Cart> FindSync(string userId);
        Task AddSync(Cart shoppingCart);        
        Task<Cart> UpdateSync(Cart shoppingCart);
    }
}
