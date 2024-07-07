using ShoppingCart.Domain.Entites;

namespace ShoppingCart.Domain.Interfaces
{
    public interface IProductCatalogClient
    {
        public Task<IEnumerable<CartItem>> GetShoppingCartItems(string[] productCatalogIds);
    }
}
