using GerenciadorLivraria.Domain.Entities;
using GerenciadorLivraria.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorLivraria.Infrastructure.DataBase.Repositories
{
    internal class BookRepository : IBookRepository
    {
        private readonly GerenciadorLivrariaDbContext _dbContext;

        public BookRepository(GerenciadorLivrariaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<BookEntity>> GetAll()
        {
            return await _dbContext.Books.AsNoTracking().ToListAsync();
        }
    }
}
