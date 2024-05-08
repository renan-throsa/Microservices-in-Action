using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Data;
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

        [HttpGet("{userId:int}")]
        public Domain.ShoppingCart Get(int userId)
        {
            return shoppingCartStore.GetBy(userId);
        }


        [HttpPost("{userId:int}/items")]
        public async Task<Domain.ShoppingCart> Post(int userId, [FromBody] int[] productIds)
        {
            Domain.ShoppingCart shoppingCart = shoppingCartStore.GetBy(userId);
            var shoppingCartItems = await productCatalogClient.GetShoppingCartItems(productIds);
            shoppingCartStore.Save(shoppingCart);
            shoppingCart.AddItems(shoppingCartItems, eventStore);
            return shoppingCart;
        }

        [HttpDelete("{userid:int}/items")]
        public Domain.ShoppingCart Delete(int userId, [FromBody] int[] productIds)
        {
            var shoppingCart = this.shoppingCartStore.GetBy(userId);
            shoppingCartStore.Save(shoppingCart);
            shoppingCart.RemoveItems(productIds, eventStore);
            return shoppingCart;
        }

    }
}
