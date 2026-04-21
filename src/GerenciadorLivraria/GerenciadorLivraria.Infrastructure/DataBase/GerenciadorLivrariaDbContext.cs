using GerenciadorLivraria.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorLivraria.Infrastructure.DataBase
{
    public class GerenciadorLivrariaDbContext : DbContext
    {
        public DbSet<BookEntity> Books => Set<BookEntity>();
        public DbSet<GenreEntity> Genres => Set<GenreEntity>();

        public GerenciadorLivrariaDbContext(DbContextOptions<GerenciadorLivrariaDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GenreEntity>().HasData(
                new GenreEntity { Id = new Guid("11111111-1111-1111-1111-111111111111"), Name = "Romance", TypeIdentifier = 1 },
                new GenreEntity { Id = new Guid("22222222-2222-2222-2222-222222222222"), Name = "Filosofia", TypeIdentifier = 2 },
                new GenreEntity { Id = new Guid("33333333-3333-3333-3333-333333333333"), Name = "Terror", TypeIdentifier = 3 },
                new GenreEntity { Id = new Guid("44444444-4444-4444-4444-444444444444"), Name = "Tecnologia", TypeIdentifier = 4 }
            );

            modelBuilder.Entity<BookEntity>()
                        .HasMany(b => b.Genre)
                        .WithMany(g => g.Books)
                        .UsingEntity(j => j.ToTable("BookGenre"));
        }
    }
}