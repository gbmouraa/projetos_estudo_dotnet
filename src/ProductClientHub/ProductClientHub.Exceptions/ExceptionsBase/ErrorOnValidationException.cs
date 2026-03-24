using System.Net;

namespace ProductClientHub.Exceptions.ExceptionsBase
{
    public class ErrorOnValidationException : ProductClientHubException
    {
        private readonly List<string> _errors;

        //https://chatgpt.com/share/69c1ef28-4970-8005-9494-8169c3017e81
        public ErrorOnValidationException(List<string> errors) : base(string.Empty)
        {
            _errors = errors;
        }

        public override List<string> GetErrors() => _errors;
     
        public override HttpStatusCode GetHttpStatusCode()
        {
            return HttpStatusCode.BadRequest;
        }
    }
}
