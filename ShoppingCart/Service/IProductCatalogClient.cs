using ShoppingCart.Domain;

namespace ShoppingCart.Service
{
    public interface IProductCatalogClient
    {
        public Task<IEnumerable<CartItem>> GetShoppingCartItems(int[] productCatalogIds);
    }
}
