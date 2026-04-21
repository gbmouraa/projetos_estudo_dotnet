using GerenciadorLivraria.API.Responses;
using GerenciadorLivraria.Application.Book;
using GerenciadorLivraria.Application.Book.CreateBook;
using GerenciadorLivraria.Application.Book.GetAllBooks;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorLivraria.API.Controllers.Book
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly CreateBookUseCase _createBookUseCase;
        private readonly GetAllBooksUseCase _getAllBooksUseCase;

        public BookController(CreateBookUseCase createBookUseCase, GetAllBooksUseCase getAllBooksUseCase)
        {
            _createBookUseCase = createBookUseCase;
            _getAllBooksUseCase = getAllBooksUseCase;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<BookResponse>), StatusCodes.Status200OK)]
        public ActionResult GetAll()
        {
            var response = _getAllBooksUseCase.Execute();
            return Ok(response);
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult GetById([FromRoute] Guid id)
        {
            return Ok("Success my brudaa");
        }

        [HttpPost]
        [ProducesResponseType(typeof(ErrorMessageResponseJson), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CreateBookResponse), StatusCodes.Status201Created)]
        public ActionResult Create([FromBody] CreateBookRequest request)
        {
            var response = _createBookUseCase.Execute(request);
            return Created(string.Empty, response);
        }
    }
}
