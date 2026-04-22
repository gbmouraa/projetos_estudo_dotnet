using GerenciadorLivraria.Domain.Entities;
using GerenciadorLivraria.Domain.Enums;
using GerenciadorLivraria.Infrastructure.DataBase;
using GerenciadorLivraria.Application.Common.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorLivraria.Application.Book.UpdateBook
{
    public class UpdateBookUseCase
    {
        private readonly GerenciadorLivrariaDbContext _dbContext;

        public UpdateBookUseCase(GerenciadorLivrariaDbContext database)
        {
            _dbContext = database;
        }

        public void Execute(Guid bookId, BookRequest request)
        {
            Validate(request);

            var genres = _dbContext.Genres
                                   .Where(g => request.Genre
                                   .Contains((EnumGenre)g.TypeIdentifier))
                                   .ToList();

            BookEntity? book = _dbContext.Books
                                          .Include(b => b.Genre)
                                          .FirstOrDefault(b => b.Id == bookId);

            if (book == null)
                throw new NotFoundException("Livro não encontrado");

            book.Title = request.Title;
            book.Author = request.Author;
            book.Genre = genres;
            book.Price = request.Price;
            book.Stock = request.Stock;
            book.UpdatedAt = DateTime.Now;

            _dbContext.Update(book);
            _dbContext.SaveChanges();
        }

        public void Validate(BookRequest model)
        {
            var validator = new BookValidator();
            var result = validator.Validate(model);

            if (!result.IsValid)
            {
                var errors = result.Errors
                                   .Select(x => x.ErrorMessage)
                                   .ToList();

                throw new ErrorOnValidationException(errors);
            }
        }
    }
}
