using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Data;
using ShoppingCart.Domain;

namespace ShoppingCart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventFeedController : ControllerBase
    {
        private readonly IEventStore eventStore;

        public EventFeedController(IEventStore eventStore) =>
          this.eventStore = eventStore;



        [HttpGet]
        public ActionResult<IEnumerable<Event>> GetEvents([FromQuery] int start, [FromQuery] int end)
        {
            if (start < 0 || end < start)
                return BadRequest();

            return Ok(eventStore.GetEvents(start, end));
        }
    }

}
