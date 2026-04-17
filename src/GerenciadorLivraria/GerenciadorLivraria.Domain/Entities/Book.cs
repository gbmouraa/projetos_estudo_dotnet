using GerenciadorLivraria.Domain.Enums;

namespace GerenciadorLivraria.Domain.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public List<EnumGenre> Genre { get; set; } = new List<EnumGenre>();
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }
    }
}
