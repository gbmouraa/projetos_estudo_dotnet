using GerenciadorLivraria.Communication.Responses;
using GerenciadorLivraria.Domain.Entities;

namespace GerenciadorLivraria.Application.UseCases.Book.GetById
{
    public interface IGetBookByIdUseCase
    {
        Task<BookResponse> Execute(Guid id);
    }
}
