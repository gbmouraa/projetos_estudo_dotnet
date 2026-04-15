using System.Net;

namespace GerenciadorLivraria.Application.Exceptions
{
    public abstract class GerenciadorLivrariaException : SystemException
    {
        public GerenciadorLivrariaException(string message) : base(message) { }

        public abstract List<string> GetErrors();
        public abstract HttpStatusCode GetHttpStatusCode();
    }
}
