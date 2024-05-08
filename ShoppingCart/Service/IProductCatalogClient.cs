using ShoppingCart.Domain;

namespace ShoppingCart.Service
{
    public interface IProductCatalogClient
    {
        public Task<IEnumerable<ShoppingCartItem>> GetShoppingCartItems(int[] productCatalogIds);
    }
}
