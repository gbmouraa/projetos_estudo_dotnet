using GerenciadorLivraria.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorLivraria.Infrastructure.DataBase
{
    public class GerenciadorLivrariaDbContext : DbContext
    {
        public DbSet<BookEntity> Books => Set<BookEntity>();

        public GerenciadorLivrariaDbContext(DbContextOptions<GerenciadorLivrariaDbContext> options) : base(options) { }
    }
}