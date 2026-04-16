using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GerenciadorLivraria.Application.Book.CreateBook;
using GerenciadorLivraria.API.Requests.Book;
using GerenciadorLivraria.API.Responses.Book;
using GerenciadorLivraria.API.Responses;

namespace GerenciadorLivraria.API.Controllers.Book
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
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
        [ProducesResponseType(typeof(ResponseErrorMessageJson), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CreateBookResponseJson), StatusCodes.Status201Created)]
        public ActionResult Create([FromBody] CreateBookRequestJson request)
        {
            var service = new CreateBookService();

            // criar mapper
            var result = service.Execute(new CreateBookModel
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
