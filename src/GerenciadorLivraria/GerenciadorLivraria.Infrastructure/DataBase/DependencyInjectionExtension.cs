using GerenciadorLivraria.Domain.Repositories;
using GerenciadorLivraria.Infrastructure.DataBase.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace GerenciadorLivraria.Infrastructure.DataBase
{
    public static class DependencyInjectionExtension
    {
        public static void AddInfrastructure(this IServiceCollection service)
        {
            service.AddScoped<IBookRepository, BookRepository>();
        }

        // add repositories
    }
}
