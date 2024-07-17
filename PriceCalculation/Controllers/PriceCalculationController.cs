using Microsoft.AspNetCore.Mvc;
using PriceCalculation.Domain;
using PriceCalculation.Domain.Interfaces;
using PriceCalculation.Domain.Models;
using System.Net;



namespace PriceCalculation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PriceCalculationController : ControllerBase
    {

        private readonly IPriceCalculationService _priceCalculationService;
        // POST api/<ValuesController>

        public PriceCalculationController(IPriceCalculationService priceCalculationService)
        {
            _priceCalculationService = priceCalculationService;
        }

        [HttpPost]
        public async Task<ActionResult<PriceCalculationViewModel>> Post([FromBody] PriceCalculationPostModel model)
        {
            var result = await _priceCalculationService.CarryOut(model);
            return CustomResponse(result);
        }

        private ActionResult CustomResponse(OperationResultModel result)
        {
            if (!result.IsValid)
            {
                return ErrorResponse(result);
            }

            return Ok(result.Content);
        }

        private ActionResult ErrorResponse(OperationResultModel result)
        {
            var content = result.Content;
            switch (result.Status)
            {
                case HttpStatusCode.BadRequest:
                    return BadRequest(content);

                case HttpStatusCode.NotFound:
                    return NotFound(content);

                case HttpStatusCode.Unauthorized:
                    return Unauthorized(content);

                case HttpStatusCode.Conflict:
                    return Conflict(content);

                case HttpStatusCode.NoContent:
                    return NoContent();

                default:
                    return Problem(statusCode: ((int)result.Status), detail: (string)content);
            }
        }

    }
}
