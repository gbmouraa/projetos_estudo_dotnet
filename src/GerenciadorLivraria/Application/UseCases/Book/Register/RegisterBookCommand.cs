using GerenciadorLivraria.Communication.Responses;
using MediatR;

namespace GerenciadorLivraria.Application.UseCases.Book.Register
{
    public class RegisterBookCommand : IRequest<RegisterBookResponse>
    {
        public string Title { get; set; } 
        public string Author { get; set; } 
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
}
