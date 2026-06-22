using GerenciadorLivraria.Communication.Responses;

namespace GerenciadorLivraria.Application.UseCases.Book.GetAll
{
    public interface IGetAllBooksUseCase
    {
        Task<List<BookResponse>> Execute();
    }
}
