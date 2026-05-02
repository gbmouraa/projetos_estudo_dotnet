using GerenciadorLivraria.API.Responses;
using GerenciadorLivraria.Application.Book.Delete;
using GerenciadorLivraria.Application.Book.GetAll;
using GerenciadorLivraria.Application.Book.GetById;
using GerenciadorLivraria.Application.Book.Register;
using GerenciadorLivraria.Application.Book.Update;
using GerenciadorLivraria.Communication.Requests;
using GerenciadorLivraria.Communication.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorLivraria.API.Controllers.Book
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BookController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<BookResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorMessageResponseJson), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetAll()
        {
            var response = await _mediator.Send(new GetAllBooksQuery());
            return Ok(response);
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(BookResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorMessageResponseJson), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetById([FromRoute] Guid id)
        {
            var response = await _mediator.Send(new GetBookByIdQuery { Id = id });
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ErrorMessageResponseJson), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(RegisterBookResponse), StatusCodes.Status201Created)]
        public async Task<ActionResult> Register([FromBody] RegisterBookRequest request)
        {
            var command = new RegisterBookCommand
            {
                Title = request.Title,
                Author = request.Author,
                Price = request.Price,
                Stock = request.Stock
            };

            var response = await _mediator.Send(command);
            return Created(string.Empty, response);
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(typeof(ErrorMessageResponseJson), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> Delete([FromRoute] Guid id)
        {
            await _mediator.Send(new DeleteBookCommand { Id = id });
            return NoContent();
        }

        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(typeof(ErrorMessageResponseJson), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorMessageResponseJson), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> Update([FromRoute] Guid id, [FromBody] UpdateBookRequest request)
        {
            var command = new UpdateBookCommand
            {
                Id = id,
                Title = request.Title,
                Author = request.Author,
                Price = request.Price,
                Stock = request.Stock
            };

            await _mediator.Send(command);

            return NoContent();
        }
    }
}
