using GerenciadorLivraria.API.Responses;
using GerenciadorLivraria.Application.UseCases.Book.Delete;
using GerenciadorLivraria.Application.UseCases.Book.GetAll;
using GerenciadorLivraria.Application.UseCases.Book.GetById;
using GerenciadorLivraria.Application.UseCases.Book.Register;
using GerenciadorLivraria.Application.UseCases.Book.Update;
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
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> GetAll([FromServices] IGetAllBooksUseCase useCase)
        {
            var response = await useCase.Execute();
            return Ok(response);
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(BookResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorMessageResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetById([FromRoute] Guid id)
        {
            var response = await _mediator.Send(new GetBookByIdQuery { Id = id });
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ErrorMessageResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(RegisterBookResponse), StatusCodes.Status201Created)]
        public async Task<ActionResult> Register([FromBody] RegisterBookRequest request, [FromServices] IRegisterBookUseCase useCase)
        {
            var response = await useCase.Execute(request);
            return Created(string.Empty, response);
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(typeof(ErrorMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> Delete([FromRoute] Guid id)
        {
            await _mediator.Send(new DeleteBookCommand { Id = id });
            return NoContent();
        }

        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(typeof(ErrorMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorMessageResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> Update([FromRoute] Guid id, [FromBody] UpdateBookRequest request, [FromServices] IUpdateBookUseCase useCase)
        {
            await useCase.Execute(request, id);

            return NoContent();
        }
    }
}
