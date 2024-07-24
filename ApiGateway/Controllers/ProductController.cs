using ClientGateway.Domain.Interfaces;
using ClientGateway.Domain.Models;
using Microsoft.AspNetCore.Mvc;


namespace ClientGateway.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IProductCatalogClient _productCatalogClient;

        public ProductController(IProductCatalogClient productCatalogClient)
        {
            _productCatalogClient = productCatalogClient;
        }

        [HttpGet]
        public async Task<IEnumerable<ProductViewModel>> Get()
        {
            return await _productCatalogClient.Get();
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public async Task<ProductViewModel> Get(string id)
        {
            return await _productCatalogClient.Get(id);
        }

        [HttpPatch]
        public async Task<ProductViewModel> Patch([FromBody] ProductPatchModel model)
        {
            return await _productCatalogClient.Update(model);
        }

    }
}
