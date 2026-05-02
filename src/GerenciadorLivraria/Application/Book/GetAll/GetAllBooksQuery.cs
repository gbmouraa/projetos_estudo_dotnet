using GerenciadorLivraria.Communication.Responses;
using MediatR;

namespace GerenciadorLivraria.Application.Book.GetAll
{
    public class GetAllBooksQuery : IRequest<List<BookResponse>> { }
}
