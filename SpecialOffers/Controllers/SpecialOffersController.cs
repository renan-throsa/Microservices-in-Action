using Microsoft.AspNetCore.Mvc;
using SpecialOffers.Domain.Interfaces;
using SpecialOffers.Domain.Models;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SpecialOffers.Controllers
{
    
    public class SpecialOffersController : BaseController
    {
        private readonly ISpecialOfferService _specialOfferService;

        public SpecialOffersController(ISpecialOfferService specialOfferService)
        {
            _specialOfferService = specialOfferService;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<SpecialOfferViewModel>> GetOffer(string id) {

            var result = await _specialOfferService.FindSync(id); ;
            return CustomResponse(result);
        }      

        
    }
}
