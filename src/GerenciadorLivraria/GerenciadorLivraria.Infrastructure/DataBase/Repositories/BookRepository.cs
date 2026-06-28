using GerenciadorLivraria.Domain.Entities;
using GerenciadorLivraria.Domain.Repositories.Book;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

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

        public async Task<BookEntity?> GetById(Guid id)
        {
            return _dbContext.Books.FirstOrDefault(b => b.Id == id);
        }

        public void Update(BookEntity book)
        {
            _dbContext.Books.Update(book);
        }

        public async Task<bool> Delete(Guid id)
        {
            var result = await _dbContext.Books.FirstOrDefaultAsync(b => b.Id == id);

            if (result is null) return false;

            _dbContext.Books.Remove(result);
            return true;
        }
    }
}
