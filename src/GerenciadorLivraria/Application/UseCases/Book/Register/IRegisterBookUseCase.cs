using GerenciadorLivraria.Communication.Requests;
using GerenciadorLivraria.Communication.Responses;

namespace GerenciadorLivraria.Application.UseCases.Book.Register
{
    public interface IRegisterBookUseCase
    {
        Task<RegisterBookResponse> Execute(RegisterBookRequest request);
    }
}
