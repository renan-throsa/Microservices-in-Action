using ShoppingCart.Domain.Models;

namespace ShoppingCart.Domain.Interfaces
{
    public interface IProductCatalogClient
    {
        public Task<IEnumerable<CartItemViewModel>> GetShoppingCartItems(IEnumerable<string> productCatalogIds);
    }
}
