using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Domain.Interfaces;
using ProductCatalog.Domain.Models;
using System.Net;
using System.Text.Json;


namespace ProductCatalog.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }

        [ResponseCache(Duration = 3600, Location = ResponseCacheLocation.Client)]
        [HttpGet]
        public ActionResult<IEnumerable<ProductViewModel>> Get()
        {            
            return CustomResponse(_service.All());
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<IEnumerable<ProductViewModel>>> Get([FromRoute] string Id)
        {
            var response = await _service.FindSync(Id);
            return CustomResponse(response);
        }

        [HttpPost("Query")]
        public async Task<ActionResult<OperationResultModel>> Get([FromBody] string[] ids)
        {
            var response = await _service.FindSync(ids);
            return CustomResponse(response);
        }        

        [HttpPatch]
        public async Task<ActionResult<OperationResultModel>> Patch([FromBody] ProductPatchModel model)
        {
            var response = await _service.UpdateSync(model);
            return CustomResponse(response);
        }

        private ActionResult CustomResponse(OperationResultModel result)
        {
            var jsonOptions = new JsonSerializerOptions();
            jsonOptions.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;

            if (!result.IsValid)
            {
                return ErrorResponse(result);
            }

            return Ok(result.Content ?? string.Empty);
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

                default:
                    return BadRequest(content);
            }
        }
    }
}
