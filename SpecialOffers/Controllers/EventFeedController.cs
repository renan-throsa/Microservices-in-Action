using Microsoft.AspNetCore.Mvc;
using SpecialOffers.Domain.Interfaces;
using SpecialOffers.Domain.Models;

namespace SpecialOffers.Controllers
{
    
    public class EventFeedController : BaseController
    {
        private readonly IEventService _service;

        public EventFeedController(IEventService service) { _service = service; }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SpecialOfferViewModel>>> GetEvents([FromQuery] int start, [FromQuery] int end)
        {            
            return CustomResponse(await _service.GetEvents(start, end));
        }

    }
}
