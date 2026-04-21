using System.Net;

namespace GerenciadorLivraria.Application.Common.Exceptions
{
    public class ErrorOnValidationException : GerenciadorLivrariaException
    {
        private readonly List<string> _errors = new List<string>();

        public ErrorOnValidationException(List<string> errors) : base(string.Empty)
        {
            _errors = errors;
        }

        public override List<string> GetErrors() => _errors;
        public override HttpStatusCode GetHttpStatusCode() => HttpStatusCode.BadRequest;
    }
}
