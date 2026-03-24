using System.Net;

namespace ProductClientHub.Exceptions.ExceptionsBase
{
    public class EmailInUseException : ProductClientHubException
    {
        private static string ErrorMessage => "Este email já está sendo usado";

        public EmailInUseException() : base(ErrorMessage) { }

        public override List<string> GetErrors() => new List<string> { Message };

        public override HttpStatusCode GetHttpStatusCode() => HttpStatusCode.BadRequest;
    }
}
