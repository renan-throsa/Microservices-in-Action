using ShoppingCart.Domain.Models;

namespace ShoppingCart.Domain.Interfaces
{
    public interface IShoppingCartService
    {
        Task<ResponseModel> FindSync(string userId);
        Task<ResponseModel> AddAsync(CartPostModel model);
        Task<ResponseModel> DeleteAsync(CartPostModel model);
    }
}
