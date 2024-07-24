using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Domain.Entities;
using ShoppingCart.Domain.Interfaces;

namespace ShoppingCart.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EventFeedController : ControllerBase
    {
        private readonly IEventRepository eventStore;

        public EventFeedController(IEventRepository eventStore) =>
          this.eventStore = eventStore;



        [HttpGet]
        public async Task<ActionResult<IEnumerable<Event>>> GetEvents([FromQuery] int start, [FromQuery] int end)
        {
            if (start < 0 || end < start)
                return BadRequest();

            return Ok(await eventStore.GetEvents(start, end));
        }
    }

}
