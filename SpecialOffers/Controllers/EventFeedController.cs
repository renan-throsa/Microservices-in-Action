using Microsoft.AspNetCore.Mvc;
using SpecialOffers.Domain;

namespace SpecialOffers.Controllers
{
    [Route("[controller]")]
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
