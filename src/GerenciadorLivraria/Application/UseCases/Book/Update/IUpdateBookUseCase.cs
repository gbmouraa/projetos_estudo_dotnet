using GerenciadorLivraria.Communication.Requests;

namespace GerenciadorLivraria.Application.UseCases.Book.Update
{
    public interface IUpdateBookUseCase
    {
        Task Execute(UpdateBookRequest request, Guid Id);
    }
}
