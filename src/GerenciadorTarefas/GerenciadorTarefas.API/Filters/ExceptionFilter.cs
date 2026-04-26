using GerenciadorTarefas.Communication.Responses;
using GerenciadorTarefas.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace GerenciadorTarefas.API.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is GerenciadorTarefasException gerenciadorTarefasException)
            {
                context.HttpContext.Response.StatusCode = (int)gerenciadorTarefasException.GetHttpStatusCode();
                context.Result = new ObjectResult(new ErrorMessageResponseJson(gerenciadorTarefasException.GetErrors()));
            }
            else
            {
                ThrowUnknowException(context);
            }
        }

        private void ThrowUnknowException(ExceptionContext context)
        {
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Result = new ObjectResult(new ErrorMessageResponseJson("ERRO DESCONHECIDO"));
        }
    }
}
