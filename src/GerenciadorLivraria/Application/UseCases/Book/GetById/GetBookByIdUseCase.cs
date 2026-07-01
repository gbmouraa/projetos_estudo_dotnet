using AutoMapper;
using GerenciadorLivraria.Application.Common.Exceptions;
using GerenciadorLivraria.Communication.Responses;
using GerenciadorLivraria.Domain.Repositories.Book;

namespace GerenciadorLivraria.Application.UseCases.Book.GetById
{
    public class GetBookByIdUseCase : IGetBookByIdUseCase
    {
        private readonly IBookRepository _repository;
        private readonly IMapper _mapper;

        public GetBookByIdUseCase(IBookRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<BookResponse> Execute(Guid id)
        {
            var book = await _repository.GetById(id);

            if (book is null) throw new NotFoundException("Livro não encontrado.");

            return _mapper.Map<BookResponse>(book);
        }
    }
}
