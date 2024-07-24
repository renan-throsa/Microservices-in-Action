using ShoppingCart.Domain.Entities;

namespace ShoppingCart.Domain.Interfaces
{
    public interface IShoppingCartRepository
    {
        Task<Cart> FindSync(string userId);
        Task AddSync(Cart shoppingCart);        
        Task<Cart> UpdateSync(Cart shoppingCart);
        IEnumerable<Cart> FindBy(string productId);
    }
}
