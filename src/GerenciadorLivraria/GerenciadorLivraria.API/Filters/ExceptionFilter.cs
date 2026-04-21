using GerenciadorLivraria.API.Responses;
using GerenciadorLivraria.Application.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GerenciadorLivraria.API.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is GerenciadorLivrariaException gerenciadorLivrariaException)
            {
                context.HttpContext.Response.StatusCode = (int)gerenciadorLivrariaException.GetHttpStatusCode();
                context.Result = new ObjectResult(new ErrorMessageResponseJson(gerenciadorLivrariaException.GetErrors()));
            }
            else
            {
                ThrowUnknowError(context);
            }
        }

        private void ThrowUnknowError(ExceptionContext context)
        {
            context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Result = new ObjectResult(new ErrorMessageResponseJson("ERRO DESCONHECIDO"));
        }
    }
}
