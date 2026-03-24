using Microsoft.AspNetCore.Mvc;
using ProductClientHub.API.UseCases.Clients.Register;
using ProductClientHub.Communication.Requests.Clients;
using ProductClientHub.Communication.Responses;
using ProductClientHub.Communication.Responses.Clients;

namespace ProductClientHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseShortClientJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorMessageJson), StatusCodes.Status400BadRequest)]
        public IActionResult Register([FromBody] RequestClientJson request)
        {
            var useCase = new RegisterClientUseCase();

            var response = useCase.Execute(request);

            return Created("", response);
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
