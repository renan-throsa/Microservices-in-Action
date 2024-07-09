using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Domain;
using ProductCatalog.Services;
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
        [HttpGet("GetAll")]
        public ActionResult<IEnumerable<ProductViewModel>> Get()
        {
            var response = _service.All();
            return CustomResponse(response);
        }

        [HttpGet("GetOne/{Id}")]
        public async Task<ActionResult<IEnumerable<ProductViewModel>>> Get([FromRoute] string Id)
        {
            var response = await _service.FindSync(Id);
            return CustomResponse(response);
        }

        [HttpGet("GetMany")]
        public async Task<ActionResult<OperationResultModel>> Get([FromQuery] string[] id)
        {
            var response = await _service.FindSync(id);
            return CustomResponse(response);
        }

        // POST api/<ProductController>
        [HttpPost]
        public void Post([FromBody] string value)
        {

        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
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
                case ResponseStatus.BadRequest:
                    return BadRequest(content);

                case ResponseStatus.NotFound:
                    return NotFound(content);

                case ResponseStatus.Unauthorized:
                    return Unauthorized(content);

                case ResponseStatus.Conflict:
                    return Conflict(content);

                default:
                    return BadRequest(content);
            }
        }
    }
}
