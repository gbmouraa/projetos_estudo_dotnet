using System.ComponentModel.DataAnnotations;

namespace GerenciadorLivraria.Domain.Entities
{
    public class BookEntity
    {
        public Guid Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Author { get; set; } = string.Empty;

        public ICollection<GenreEntity> Genre { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int Stock { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; private set; }
    }
}
