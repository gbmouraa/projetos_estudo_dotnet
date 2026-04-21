using System.Net;

namespace GerenciadorLivraria.Application.Common.Exceptions
{
    public class NotFoundException : GerenciadorLivrariaException
    {
        public NotFoundException(string message) : base(message) { }

        public override List<string> GetErrors() => new List<string> { Message };
        public override HttpStatusCode GetHttpStatusCode() => HttpStatusCode.NotFound;
    }
}
