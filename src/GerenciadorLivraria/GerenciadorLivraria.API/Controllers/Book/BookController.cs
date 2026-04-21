using GerenciadorLivraria.API.Requests.Book;
using GerenciadorLivraria.API.Responses;
using GerenciadorLivraria.API.Responses.Book;
using GerenciadorLivraria.Application.Book.CreateBook;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorLivraria.API.Controllers.Book
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly CreateBookUseCase _createBookUseCase;

        public BookController(CreateBookUseCase createBookService)
        {
            _createBookUseCase = createBookService;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            return Ok("Success my brudaa");
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult GetById([FromRoute] Guid id)
        {
            return Ok("Success my brudaa");
        }

        [HttpPost]
        [ProducesResponseType(typeof(ErrorMessageResponseJson), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CreateBookResponseJson), StatusCodes.Status201Created)]
        public ActionResult Create([FromBody] CreateBookRequestJson request)
        {
            // criar mapper
            var result = _createBookUseCase.Execute(new CreateBookRequest
            {
                Title = request.Title,
                Author = request.Author,
                Genre = request.Genre,
                Price = request.Price,
                Stock = request.Stock,
            });

            var response = new CreateBookResponseJson
            {
                Id = result.Id,
                Title = result.Title
            };

            return Created(string.Empty, response);
        }
    }
}
