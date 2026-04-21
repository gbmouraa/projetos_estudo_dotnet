using GerenciadorLivraria.Application.Common.Exceptions;
using GerenciadorLivraria.Domain.Entities;
using GerenciadorLivraria.Infrastructure.DataBase;

namespace GerenciadorLivraria.Application.Book.DeleteBook
{
    public class DeleteBookUseCase
    {
        private readonly GerenciadorLivrariaDbContext _dbContext;

        public DeleteBookUseCase(GerenciadorLivrariaDbContext database)
        {
            _dbContext = database;
        }

        public void Execute(Guid requestId)
        {
            BookEntity? book = _dbContext.Books
                                         .Where(b => b.Id == requestId)
                                         .FirstOrDefault();

            if (book == null)
                throw new NotFoundException("Livro não encontrado.");

            _dbContext.Remove(book);
            _dbContext.SaveChanges();
        }
    }
}
