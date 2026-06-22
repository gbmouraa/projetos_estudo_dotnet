using MediatR;

namespace GerenciadorLivraria.Application.UseCases.Book.Update
{
    public class UpdateBookCommand : IRequest<Unit>
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
}
