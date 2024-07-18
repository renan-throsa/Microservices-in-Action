using ClientGateway.Domain.Models;

namespace ClientGateway.Domain.Interfaces
{
    public interface IShopingCartClient
    {
        public Task<CartViewModel> Get(string userId);
        public Task<CartViewModel> PostItems(CartPostModel model);
        public Task<CartViewModel> DeleteItems(CartPostModel model);
    }
}
