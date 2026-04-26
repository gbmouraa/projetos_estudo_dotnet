using System.Net;

namespace GerenciadorTarefas.Exceptions
{
    public class ErrorOnValidationException : GerenciadorTarefasException
    {
        public List<string> Errors { get; private set; }

        public ErrorOnValidationException(List<string> errors) : base(string.Empty)
        {
            Errors = errors;
        }

        public override List<string> GetErrors() => Errors;
        public override HttpStatusCode GetHttpStatusCode() => HttpStatusCode.BadRequest;
    }
}
