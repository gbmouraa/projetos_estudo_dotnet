using GerenciadorLivraria.Application.Common.Exceptions;
using GerenciadorLivraria.Domain.Repositories;
using GerenciadorLivraria.Domain.Repositories.Book;

namespace GerenciadorLivraria.Application.UseCases.Book.Delete
{
    public class DeleteBookUseCase : IDeleteBookUseCase
    {
        private readonly IBookRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteBookUseCase(IBookRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }


        public async Task Execute(Guid id)
        {
            var result = await _repository.Delete(id);

            if (!result)
            {
                throw new NotFoundException("Não foi encontrado nenhum livro com o Id informado");
            }

            await _unitOfWork.Commit();
        }
    }
}
