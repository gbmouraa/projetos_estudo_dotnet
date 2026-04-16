namespace GerenciadorLivraria.API.Requests.Book
{
    public class CreateBookRequestJson
    {
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public List<string> Genre { get; set; } = new List<string>();
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
}
