using AutoMapper;
using GerenciadorLivraria.Communication.Responses;
using GerenciadorLivraria.Domain.Repositories.Book;

namespace GerenciadorLivraria.Application.UseCases.Book.GetAll
{
    public class GetAllBooksUseCase : IGetAllBooksUseCase
    {
        private readonly IBookRepository _repository;
        private readonly IMapper _mapper;

        public GetAllBooksUseCase(IBookRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<BookResponse>> Execute()
        {
            var books = await _repository.GetAll();

            return _mapper.Map<List<BookResponse>>(books);
        }
    }
}
