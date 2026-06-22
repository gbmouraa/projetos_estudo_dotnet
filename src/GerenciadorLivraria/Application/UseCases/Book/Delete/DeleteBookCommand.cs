using MediatR;

namespace GerenciadorLivraria.Application.UseCases.Book.Delete
{
    public class DeleteBookCommand : IRequest<Unit>
    {
        public required Guid Id { get; set; }
    }
}
