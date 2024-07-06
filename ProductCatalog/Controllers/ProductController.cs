using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Domain;
using ProductCatalog.Services;


namespace ProductCatalog.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            this._service = service;
        }
        
        [HttpGet("GetAll")]
        public ActionResult<Response> Get()
        {
            return Ok(_service.All());
        }

        [HttpGet("GetOne/{Id}")]
        public async Task<ActionResult<Response>> Get([FromRoute] string Id)
        {
            var response = await _service.FindSync(Id);
            if (response.Status == ResponseStatus.Found) return Ok(response);
            return ErrorResponse(response);
        }        

        [HttpGet("GetMany")]
        public async Task<ActionResult<Response>> Get([FromQuery] string[] ids)
        {
            var response = await _service.FindSync(ids);
            if (response.Status == ResponseStatus.Ok) return Ok(response);
            return ErrorResponse(response);
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

        private ActionResult<Response> ErrorResponse(Response result)
        {
            switch (result.Status)
            {
                case ResponseStatus.BadRequest:
                    return BadRequest(result);

                case ResponseStatus.NotFound:
                    return NotFound(result);

                case ResponseStatus.Unauthorized:
                    return Unauthorized(result);

                case ResponseStatus.Conflict:
                    return Conflict(result);

                default:
                    return BadRequest(result);
            }
        }
    }
}
