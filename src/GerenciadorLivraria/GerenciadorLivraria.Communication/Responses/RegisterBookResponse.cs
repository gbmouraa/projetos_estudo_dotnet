namespace GerenciadorLivraria.Communication.Responses
{
    public class RegisterBookResponse
    {
        public required Guid Id { get; set; }
        public required string Title { get; set; } = string.Empty;
    }
}
