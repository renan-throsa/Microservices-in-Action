using ShoppingCart.Domain.Models;

namespace ShoppingCart.Domain.Interfaces
{
    public interface IProductCatalogClient
    {
        public Task<IEnumerable<CartItemViewModel>> Query(IEnumerable<string> productCatalogIds);
    }
}
