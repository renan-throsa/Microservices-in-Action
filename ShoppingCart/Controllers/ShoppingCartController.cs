using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Domain.Interfaces;
using ShoppingCart.Domain.Models;

namespace ShoppingCart.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartService _shoppingCartService;

        public ShoppingCartController(IShoppingCartService shoppingCartService)
        {
            this._shoppingCartService = shoppingCartService;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<ResponseModel>> Get(string userId)
        {
            return await _shoppingCartService.FindSync(userId);
        }


        [HttpPost("items")]
        public async Task<ActionResult<ResponseModel>> Post(CartPostModel model)
        {
            
            return await _shoppingCartService.AddAsync(model);
        }

        [HttpDelete("items")]
        public async Task<ActionResult<ResponseModel>> Delete(CartPostModel model)
        {
            return await _shoppingCartService.DeleteAsync(model);
        }

    }
}
