using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GerenciadorLivraria.Application.Book.CreateBook;
using GerenciadorLivraria.Application.Requests;

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
        public ActionResult Create(CreateBookRequestJson request)
        {
            var service = new CreateBookService();

            var result = service.Execute(new CreateBookModel
            {
                Title = request.Title,
                Author = request.Author,
                Genre = request.Genre,
                Price = request.Price,
                Stock = request.Stock,
            });

            return Created(string.Empty, result);
        }
    }
}
