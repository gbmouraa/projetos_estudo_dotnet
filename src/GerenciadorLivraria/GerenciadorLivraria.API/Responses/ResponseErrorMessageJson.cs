namespace GerenciadorLivraria.API.Responses
{
    public class ResponseErrorMessageJson
    {
        public List<string> Errors { get; private set; }

        public ResponseErrorMessageJson(List<string> messages) { Errors = messages; }

        public ResponseErrorMessageJson(string error)
        {
            Errors = new List<string> { error };
        }

    }
}
