namespace GerenciadorLivraria.Communication.Responses
{
    public class BookResponse
    {
        public required Guid Id { get; set; }
        public required string Title { get; set; }
        public required string Author { get; set; }
        public required decimal Price { get; set; }
        public required int Stock { get; set; }
    }

}
