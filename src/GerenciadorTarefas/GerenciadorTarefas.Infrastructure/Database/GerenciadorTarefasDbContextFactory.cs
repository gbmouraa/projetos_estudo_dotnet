using GerenciadorTarefas.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace GerenciadorTarefas.Infrastructure.DataBase
{
    public class GerenciadorTarefasDbContextFactory : IDesignTimeDbContextFactory<GerenciadorTarefasDbContext>
    {
        public GerenciadorTarefasDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<GerenciadorTarefasDbContext>();
            optionsBuilder.UseSqlite("Data Source=Data/GerenciadorTarefas.db");

            return new GerenciadorTarefasDbContext(optionsBuilder.Options);
        }
    }
}