using ShoppingCart.Domain.Models;

namespace ShoppingCart.Domain.Interfaces
{
    public interface IProductCatalogClient
    {
        Task<IEnumerable<CartItemViewModel>> Query(IEnumerable<string> productCatalogIds);
        Task<CartItemViewModel> Find(string productCatalogId);
    }
}
