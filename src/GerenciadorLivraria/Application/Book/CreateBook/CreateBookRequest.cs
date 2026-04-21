using GerenciadorLivraria.Domain.Enums;

namespace GerenciadorLivraria.Application.Book.CreateBook
{
    public class CreateBookRequest
    {
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public List<EnumGenre> Genre { get; set; } = new List<EnumGenre>();
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
}
