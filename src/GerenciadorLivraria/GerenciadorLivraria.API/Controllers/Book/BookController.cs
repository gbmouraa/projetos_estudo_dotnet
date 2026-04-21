using GerenciadorLivraria.API.Responses;
using GerenciadorLivraria.Application.Book;
using GerenciadorLivraria.Application.Book.CreateBook;
using GerenciadorLivraria.Application.Book.DeleteBook;
using GerenciadorLivraria.Application.Book.GetAllBooks;
using GerenciadorLivraria.Application.Book.GetBookById;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorLivraria.API.Controllers.Book
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly CreateBookUseCase _createBookUseCase;
        private readonly GetAllBooksUseCase _getAllBooksUseCase;
        private readonly GetBookByIdUseCase _getBookByIdUseCase;
        private readonly DeleteBookUseCase _deleteBookUseCase;

        public BookController(
            CreateBookUseCase createBookUseCase,
            GetAllBooksUseCase getAllBooksUseCase,
            GetBookByIdUseCase getBookByIdUseCase,
            DeleteBookUseCase deleteBookUseCase)
        {
            _createBookUseCase = createBookUseCase;
            _getAllBooksUseCase = getAllBooksUseCase;
            _getBookByIdUseCase = getBookByIdUseCase;
            _deleteBookUseCase = deleteBookUseCase;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<BookResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorMessageResponseJson), StatusCodes.Status404NotFound)]
        public ActionResult GetAll()
        {
            var response = _getAllBooksUseCase.Execute();
            return Ok(response);
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(BookResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorMessageResponseJson), StatusCodes.Status404NotFound)]
        public ActionResult GetById([FromRoute] Guid id)
        {
            var response = _getBookByIdUseCase.Execute(id);
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ErrorMessageResponseJson), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CreateBookResponse), StatusCodes.Status201Created)]
        public ActionResult Create([FromBody] CreateBookRequest request)
        {
            var response = _createBookUseCase.Execute(request);
            return Created(string.Empty, response);
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(typeof(ErrorMessageResponseJson), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult Delete([FromRoute] Guid id)
        {
            _deleteBookUseCase.Execute(id);
            return NoContent();
        }
    }
}
