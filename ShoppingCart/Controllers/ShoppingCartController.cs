using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Data;
using ShoppingCart.Domain;
using ShoppingCart.Service;

namespace ShoppingCart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartStore shoppingCartStore;
        private readonly IProductCatalogClient productCatalogClient;
        private readonly IEventStore eventStore;


        public ShoppingCartController(IShoppingCartStore shoppingCartStore, IProductCatalogClient productCatalogClient, IEventStore eventStore)
        {
            this.shoppingCartStore = shoppingCartStore;
            this.productCatalogClient = productCatalogClient;
            this.eventStore = eventStore;
        }

        [HttpGet("{userId}")]
        [ResponseCache(Duration =7200)]
        public async Task<ActionResult<Cart>> Get(string userId)
        {
            return await shoppingCartStore.GetBy(userId);
        }


        [HttpPost("{userId}/items")]
        public async Task<ActionResult<Cart>> Post(string userId, [FromBody] int[] productIds)
        {
            var shoppingCart = await shoppingCartStore.GetBy(userId);
            var shoppingCartItems = await productCatalogClient.GetShoppingCartItems(productIds);
            await shoppingCartStore.Save(shoppingCart);
            shoppingCart.AddItems(shoppingCartItems, eventStore);
            return shoppingCart;
        }

        [HttpDelete("{userid}/items")]
        public async Task<ActionResult<Cart>> Delete(string userId, [FromBody] int[] productIds)
        {
            var shoppingCart = await shoppingCartStore.GetBy(userId);            
            shoppingCart.RemoveItems(productIds, eventStore);
            await shoppingCartStore.Save(shoppingCart);
            return shoppingCart;
        }

    }
}
