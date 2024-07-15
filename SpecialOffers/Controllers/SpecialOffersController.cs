using Microsoft.AspNetCore.Mvc;
using SpecialOffers.Domain.Interfaces;
using SpecialOffers.Domain.Models;


namespace SpecialOffers.Controllers
{

    public class SpecialOffersController : BaseController
    {
        private readonly ISpecialOfferService _specialOfferService;

        public SpecialOffersController(ISpecialOfferService specialOfferService)
        {
            _specialOfferService = specialOfferService;
        }

        [HttpGet]
        public async Task<ActionResult<SpecialOfferViewModel>> GetSpealcialOffers([FromQuery] HashSet<string> productId)
        {
            return CustomResponse(await _specialOfferService.FindOffers(productId));
        }

    }
}
