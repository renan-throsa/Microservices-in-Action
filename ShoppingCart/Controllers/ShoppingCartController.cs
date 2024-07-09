using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Domain.Interfaces;
using ShoppingCart.Domain.Models;
using System.Text.Json;

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
            var response = await _shoppingCartService.FindSync(userId); ;
            return CustomResponse(response);
        }


        [HttpPost("items")]
        public async Task<ActionResult<CartViewModel>> Post(CartPostModel model)
        {
            var result = await _shoppingCartService.SaveAsync(model);
            return CustomResponse(result);
        }

        [HttpDelete("items")]
        public async Task<ActionResult<CartViewModel>> Delete(CartPostModel model)
        {
            var result = await _shoppingCartService.DeleteAsync(model);
            return CustomResponse(result);
        }

        private ActionResult CustomResponse(OperationResultModel result)
        {
            var jsonOptions = new JsonSerializerOptions();
            jsonOptions.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;

            if (!result.IsValid)
            {
                return ErrorResponse(result);
            }

            return Ok(result.Content ?? string.Empty);
        }

        private ActionResult ErrorResponse(OperationResultModel result)
        {
            var content = result.Content;
            switch (result.Status)
            {
                case ResponseStatus.BadRequest:
                    return BadRequest(content);

                case ResponseStatus.NotFound:
                    return NotFound(content);

                case ResponseStatus.Unauthorized:
                    return Unauthorized(content);

                case ResponseStatus.Conflict:
                    return Conflict(content);

                default:
                    return BadRequest(content);
            }
        }

    }
}
