using ClientGateway.Domain.Interfaces;
using ClientGateway.Domain.Models;
using Microsoft.AspNetCore.Mvc;


namespace ClientGateway.Controllers
{
    public class PriceController : BaseController
    {

        private readonly IPriceCalculatorClient _priceCalculatorClient;

        public PriceController(IPriceCalculatorClient priceCalculatorClient)
        {
            _priceCalculatorClient = priceCalculatorClient;
        }        

        [HttpPost]
        public async Task<PriceCalculationViewModel> Post([FromBody] PriceCalculationPostModel model)
        {
            return await _priceCalculatorClient.CarryOut(model);
        }



    }
}
