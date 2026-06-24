using GerenciadorLivraria.Domain.Entities;
using GerenciadorLivraria.Domain.Repositories.Book;
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

        public async Task Add(BookEntity book)
        {
            await _dbContext.Books.AddAsync(book);
        }

        public async Task<List<BookEntity>> GetAll()
        {
            return await _dbContext.Books.AsNoTracking().ToListAsync();
        }
    }
}
