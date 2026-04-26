using System.Net;

namespace GerenciadorTarefas.Exceptions
{
    public class NotFoundException : GerenciadorTarefasException
    {
        public NotFoundException(string message) : base(message) { }

        public override List<string> GetErrors() => new List<string> { Message };
        public override HttpStatusCode GetHttpStatusCode() => HttpStatusCode.NotFound;
    }
}
