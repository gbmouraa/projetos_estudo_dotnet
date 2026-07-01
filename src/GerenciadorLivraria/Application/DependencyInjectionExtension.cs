using GerenciadorLivraria.Application.AutoMapper;
using GerenciadorLivraria.Application.UseCases.Book.Delete;
using GerenciadorLivraria.Application.UseCases.Book.GetAll;
using GerenciadorLivraria.Application.UseCases.Book.GetById;
using GerenciadorLivraria.Application.UseCases.Book.Register;
using GerenciadorLivraria.Application.UseCases.Book.Update;
using Microsoft.Extensions.DependencyInjection;

namespace GerenciadorLivraria.Application
{
    public static class DependencyInjectionExtension
    {
        public static void AddApplication(this IServiceCollection service)
        {
            AddUseCases(service);
            AddAutoMapper(service);
        }

        private static void AddUseCases(IServiceCollection service)
        {
            service.AddScoped<IGetAllBooksUseCase, GetAllBooksUseCase>();
            service.AddScoped<IRegisterBookUseCase, RegisterBookUseCase>();
            service.AddScoped<IUpdateBookUseCase, UpdateBookUseCase>();
            service.AddScoped<IGetBookByIdUseCase, GetBookByIdUseCase>();
            service.AddScoped<IDeleteBookUseCase, DeleteBookUseCase>();
        }

        private static void AddAutoMapper(IServiceCollection service)
        {
            service.AddAutoMapper(cfg => cfg.AddProfile<AutoMapping>());
        }
    }
}
