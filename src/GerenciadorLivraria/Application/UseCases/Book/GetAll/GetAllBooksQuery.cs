using GerenciadorLivraria.Communication.Responses;
using MediatR;

namespace GerenciadorLivraria.Application.UseCases.Book.GetAll
{
    public class GetAllBooksQuery : IRequest<List<BookResponse>> { }
}
