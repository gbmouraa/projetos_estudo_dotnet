using GerenciadorLivraria.Domain.Repositories;
using GerenciadorLivraria.Domain.Repositories.Book;
using GerenciadorLivraria.Infrastructure.DataBase.Repositories;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace GerenciadorLivraria.Infrastructure.DataBase
{
    public static class DependencyInjectionExtension
    {
        public static void AddInfrastructure(this IServiceCollection service, IConfiguration configuration)
        {
            AddRepositories(service);
            AddDbContext(service, configuration);
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Default");
            var connection = new SqliteConnection(connectionString);

            services.AddDbContext<GerenciadorLivrariaDbContext>(options =>
            {
                options.UseSqlite(connection);
            });
        }
    }
}
