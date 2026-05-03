namespace GerenciadorLivraria.API.Responses
{
    public class ErrorMessageResponse
    {
        public List<string> Errors { get; private set; }

        public ErrorMessageResponse(List<string> messages) { Errors = messages; }

        public ErrorMessageResponse(string error)
        {
            Errors = new List<string> { error };
        }
    }
}
