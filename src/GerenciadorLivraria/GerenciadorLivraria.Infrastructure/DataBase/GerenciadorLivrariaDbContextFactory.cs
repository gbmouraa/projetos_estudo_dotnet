using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace GerenciadorLivraria.Infrastructure.DataBase
{
    public class GerenciadorLivrariaDbContextFactory : IDesignTimeDbContextFactory<GerenciadorLivrariaDbContext>
    {
        public GerenciadorLivrariaDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<GerenciadorLivrariaDbContext>();
            optionsBuilder.UseSqlite("Data Source=\"C:\\Users\\Gabriel\\Documents\\Sqlite\\GerenciadorLivraria.db\"");

            return new GerenciadorLivrariaDbContext(optionsBuilder.Options);
        }
    }
}