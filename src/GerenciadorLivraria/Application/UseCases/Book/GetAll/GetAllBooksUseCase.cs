using GerenciadorLivraria.Communication.Responses;
using GerenciadorLivraria.Domain.Repositories;
using GerenciadorLivraria.Domain.Repositories.Book;

namespace GerenciadorLivraria.Application.UseCases.Book.GetAll
{
    public class GetAllBooksUseCase : IGetAllBooksUseCase
    {
        // mapper 
        private readonly IBookRepository _repository;

        public GetAllBooksUseCase(IBookRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<BookResponse>> Execute()
        {
            var result = await _repository.GetAll();

            return result.Select(x => new BookResponse
            {
                Author = x.Author,
                Id = x.Id,
                Price = x.Price,
                Stock = x.Stock,
                Title = x.Title,
            }
            ).ToList();
        }
    }
}
