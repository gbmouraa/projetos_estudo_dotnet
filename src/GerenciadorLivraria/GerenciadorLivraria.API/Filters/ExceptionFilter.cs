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
                HandleProjectException(context);
            else
                ThrowUnknowError(context);
        }

        private void HandleProjectException(ExceptionContext context)
        {
            var ex = (GerenciadorLivrariaException)context.Exception;
            context.HttpContext.Response.StatusCode = (int)ex.GetHttpStatusCode();
            context.Result = new ObjectResult(new ErrorMessageResponse(ex.GetErrors()));
        }

        private void ThrowUnknowError(ExceptionContext context)
        {
            context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Result = new ObjectResult(new ErrorMessageResponse("ERRO DESCONHECIDO"));
        }
    }
}
