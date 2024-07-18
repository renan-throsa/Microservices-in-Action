using ClientGateway.Domain.Models;

namespace ClientGateway.Domain.Interfaces
{
    public interface IProductCatalogClient
    {
        Task<IEnumerable<ProductViewModel>> Query(IEnumerable<string> productCatalogIds);
        Task<ProductViewModel> Get(string productCatalogId);
        Task<IEnumerable<ProductViewModel>> Get();
    }
}
