using GerenciadorLivraria.Application.UseCases.Book.GetAll;
using GerenciadorLivraria.Application.UseCases.Book.Register;
using Microsoft.Extensions.DependencyInjection;

namespace GerenciadorLivraria.Application
{
    public static class DependencyInjectionExtension
    {
        public static void AddApplication(this IServiceCollection service)
        {
            AddUseCases(service);
        }

        private static void AddUseCases(IServiceCollection service)
        {
            service.AddScoped<IGetAllBooksUseCase, GetAllBooksUseCase>();
            service.AddScoped<IRegisterBookUseCase, RegisterBookUseCase>();
        }
    }
}
