using Microsoft.AspNetCore.Mvc;
using SpecialOffers.Domain.Interfaces;
using SpecialOffers.Domain.Models;

namespace SpecialOffers.Controllers
{
    
    public class EventFeedController : BaseController
    {
        private readonly ISpecialOfferService _specialOfferService;


        public EventFeedController(ISpecialOfferService specialOfferService)
        {
            _specialOfferService = specialOfferService;
        }        

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SpecialOfferViewModel>>> GetEvents()
        {
            return CustomResponse(await _specialOfferService.FindOffers());
        }

    }
}
