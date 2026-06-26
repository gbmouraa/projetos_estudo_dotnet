using GerenciadorLivraria.Domain.Entities;

namespace GerenciadorLivraria.Application.UseCases.Book.GetById
{
    public interface IGetBookByIdUseCase
    {
        Task<BookEntity?> Execute(Guid id);
    }
}
