using Microsoft.EntityFrameworkCore;
using GerenciadorTarefas.Domain.Entities;

namespace GerenciadorTarefas.Infrastructure.Database
{
    public class GerenciadorTarefasDbContext : DbContext
    {
        public DbSet<TaskEntity> Tasks => Set<TaskEntity>();

        public GerenciadorTarefasDbContext(DbContextOptions<GerenciadorTarefasDbContext> options) : base(options) { }
    }
}
