namespace GerenciadorTarefas.Communication.Responses
{
    public class ErrorMessageResponseJson
    {
        public List<string> Errors { get; private set; }

        public ErrorMessageResponseJson(List<string> errors)
        {
            Errors = errors;
        }

        public ErrorMessageResponseJson(string error)
        {
            Errors = new List<string> { error };
        }
    }
}
