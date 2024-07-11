using Microsoft.AspNetCore.Mvc;
using SpecialOffers.Domain;
using System.Net;

namespace SpecialOffers.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected ActionResult CustomResponse(OperationResultModel result)
        {            
            if (!result.IsValid)
            {
                return ErrorResponse(result);
            }

            return Ok(result.Content ?? string.Empty);
        }

        protected ActionResult ErrorResponse(OperationResultModel result)
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

                default:
                    return Problem(statusCode: ((int)result.Status), detail: (string)content);
            }
        }
    }
}
