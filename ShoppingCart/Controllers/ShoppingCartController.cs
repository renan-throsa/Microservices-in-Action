using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Domain.Interfaces;
using ShoppingCart.Domain.Models;
using System.Net;

namespace ShoppingCart.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartService _shoppingCartService;

        public ShoppingCartController(IShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }
        

        [HttpGet("{userId}")]
        public async Task<ActionResult<CartViewModel>> Get(string userId)
        {
            var result = await _shoppingCartService.FindSync(userId); ;
            return Customresult(result);
        }


        [HttpPost("items")]
        public async Task<ActionResult<CartViewModel>> Post(CartPostModel model)
        {
            var result = await _shoppingCartService.SaveAsync(model);
            return Customresult(result);
        }

        [HttpDelete("items")]
        public async Task<ActionResult<CartViewModel>> Delete(CartPostModel model)
        {
            var result = await _shoppingCartService.DeleteAsync(model);
            return Customresult(result);
        }

        private ActionResult Customresult(OperationResultModel result)
        {            

            if (!result.IsValid)
            {
                return Errorresult(result);
            }

            return Ok(result.Content);
        }

        private ActionResult Errorresult(OperationResultModel result)
        {
            var content = result.Content;
            switch (result.Status)
            {
                case HttpStatusCode.BadRequest:
                    return BadRequest(content);

                case HttpStatusCode.NotFound:
                    return NotFound(content);

                case HttpStatusCode.Unauthorized:
                    return Unauthorized(content);

                case HttpStatusCode.Conflict:
                    return Conflict(content);    

                default:
                    return Problem(statusCode: ((int)result.Status), detail: (string)content);
            }
        }

    }
}
