using GerenciadorLivraria.Application.Common.Exceptions;
using GerenciadorLivraria.Domain.Entities;
using GerenciadorLivraria.Domain.Repositories.Book;

namespace GerenciadorLivraria.Application.UseCases.Book.GetById
{
    public class GetBookByIdUseCase : IGetBookByIdUseCase
    {
        private readonly IBookRepository _repository;

        public GetBookByIdUseCase(IBookRepository repository)
        {
            _repository = repository;
        }

        public Task<BookEntity?> Execute(Guid id)
        {
            var result = _repository.GetById(id);

            if (result is null) throw new NotFoundException("Livro não encontrado.");

            return result;
        }
    }
}
