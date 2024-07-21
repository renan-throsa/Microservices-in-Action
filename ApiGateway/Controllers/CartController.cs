using ClientGateway.Domain.Interfaces;
using ClientGateway.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClientGateway.Controllers
{    
    public class CartController : BaseController
    {

        private readonly IShopingCartClient _shopingCartClient;

        public CartController(IShopingCartClient shopingCartClient)
        {
            _shopingCartClient = shopingCartClient;
        }
        

        // GET api/<CartController>/5
        [HttpGet("{id}")]
        public async Task<CartViewModel> Get(string id)
        {
            return await _shopingCartClient.Get(id);
        }

        // POST api/<CartController>
        [HttpPost]
        public async Task<CartViewModel> Post([FromBody] CartPostModel model)
        {
            return await _shopingCartClient.PostItems(model);
        }       

        // DELETE api/<CartController>/5
        [HttpPatch]
        public async Task<CartViewModel> Delete(CartPostModel model)
        {
            return await _shopingCartClient.DeleteItems(model);
        }
    }
}
