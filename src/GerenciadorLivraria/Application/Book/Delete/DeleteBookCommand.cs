using MediatR;

namespace GerenciadorLivraria.Application.Book.Delete
{
    public class DeleteBookCommand : IRequest<Unit>
    {
        public required Guid Id { get; set; }
    }
}
