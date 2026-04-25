using GerenciadorLivraria.Domain.Enums;

namespace GerenciadorLivraria.Application.Book
{
    public class BookResponse
    {
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public List<EnumGenre> Genre { get; set; } = [];
        public int Stock { get; set; }
        public decimal Price { get; set; }
    }
}
