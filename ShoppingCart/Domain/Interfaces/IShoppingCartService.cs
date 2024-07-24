using ShoppingCart.Domain.Models;

namespace ShoppingCart.Domain.Interfaces
{
    public interface IShoppingCartService
    {
        Task<OperationResultModel> FindSync(string userId);
        Task<OperationResultModel> SaveAsync(CartPostModel model);
        Task<OperationResultModel> DeleteAsync(CartPostModel model);
        Task UpdatedAsync(string productId);
    }
}
