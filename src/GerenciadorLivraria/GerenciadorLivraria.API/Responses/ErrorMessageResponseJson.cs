namespace GerenciadorLivraria.API.Responses
{
    public class ErrorMessageResponseJson
    {
        public List<string> Errors { get; private set; }

        public ErrorMessageResponseJson(List<string> messages) { Errors = messages; }

        public ErrorMessageResponseJson(string error)
        {
            Errors = new List<string> { error };
        }

    }
}
