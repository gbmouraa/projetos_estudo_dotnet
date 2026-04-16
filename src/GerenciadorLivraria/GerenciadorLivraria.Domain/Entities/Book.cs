using GerenciadorLivraria.Domain.Enums;

namespace GerenciadorLivraria.Domain.Entities
{
    public class Book
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public List<string> Genre { get; set; } = new List<string>();
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }
    }
}
