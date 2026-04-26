using System.Net;

namespace GerenciadorTarefas.Exceptions
{
    public abstract class GerenciadorTarefasException : Exception
    {
        protected GerenciadorTarefasException(string message) : base(message) { }

        public abstract List<string> GetErrors();
        public abstract HttpStatusCode GetHttpStatusCode();
    }
}
