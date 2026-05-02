using MediatR;

namespace GerenciadorLivraria.Application.Book.Update
{
    public class UpdateBookCommand : IRequest<Unit>
    {
        public required Guid Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
}
