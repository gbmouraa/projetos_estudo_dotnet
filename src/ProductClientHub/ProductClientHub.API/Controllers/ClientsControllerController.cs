using Microsoft.AspNetCore.Mvc;

namespace ProductClientHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsControllerController : ControllerBase
    {
        [HttpPost]
        public IActionResult Register()
        {
            return Created();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok();
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Edit([FromRoute] Guid id)
        {
            return NoContent();
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            return NoContent();
        }
    }
}
