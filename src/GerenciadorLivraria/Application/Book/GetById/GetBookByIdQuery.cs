using GerenciadorLivraria.Communication.Responses;
using MediatR;

namespace GerenciadorLivraria.Application.Book.GetById
{
    public class GetBookByIdQuery : IRequest<BookResponse>
    {
        public required Guid Id { get; set; }
    }
}
