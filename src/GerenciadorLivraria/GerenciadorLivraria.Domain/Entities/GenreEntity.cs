namespace GerenciadorLivraria.Domain.Entities
{
    public class GenreEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int TypeIdentifier { get; set; }
        public ICollection<BookEntity> Books { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
