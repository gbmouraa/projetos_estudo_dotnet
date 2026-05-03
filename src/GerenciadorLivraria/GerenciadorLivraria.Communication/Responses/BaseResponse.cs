namespace GerenciadorLivraria.Communication.Responses
{
    public abstract class BaseResponse
    {
        public required Guid Id { get; set; }
        public required string Title { get; set; }
        public required string Author { get; set; }
        public required decimal Price { get; set; }
        public required int Stock { get; set; }
    }
}
