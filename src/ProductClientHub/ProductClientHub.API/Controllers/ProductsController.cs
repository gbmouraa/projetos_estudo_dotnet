using Microsoft.AspNetCore.Mvc;
using ProductClientHub.API.UseCases.Products.Delete;
using ProductClientHub.API.UseCases.Products.Register;
using ProductClientHub.Communication.Requests.Products;
using ProductClientHub.Communication.Responses;
using ProductClientHub.Communication.Responses.Products;

namespace ProductClientHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        [HttpPost]
        [Route("{clientId}")]
        [ProducesResponseType(typeof(ResponseErrorMessageJson), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseErrorMessageJson), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResponseShortProductJson), StatusCodes.Status201Created)]
        public IActionResult Register([FromRoute] Guid clientId, [FromBody] RequestProductJson request)
        {
            var useCase = new RegisterProductUseCase();

            var response = useCase.Execute(clientId, request);

            return Created("", response);
        }

        [HttpDelete]
        [Route("{productId}")]
        [ProducesResponseType(typeof(ResponseErrorMessageJson), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Delete([FromRoute] Guid productId)
        {
            var useCase = new DeleteProductUseCase();

            useCase.Execute(productId);

            return NoContent();
        }

    }
}
