using GerenciadorLivraria.Application.Common.Exceptions;
using GerenciadorLivraria.Domain.Entities;
using GerenciadorLivraria.Domain.Enums;
using GerenciadorLivraria.Infrastructure.DataBase;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorLivraria.Application.Book.GetBookById
{
    public class GetBookByIdUseCase
    {
        private readonly GerenciadorLivrariaDbContext _dbContext;

        public GetBookByIdUseCase(GerenciadorLivrariaDbContext database)
        {
            _dbContext = database;
        }

        public BookResponse Execute(Guid requestId)
        {
            BookEntity? book = _dbContext.Books
                                         .Where(b => b.Id == requestId)
                                         .Include(b => b.Genre)
                                         .FirstOrDefault();

            if (book == null)
                throw new NotFoundException("Livro não encontrado.");

            return new BookResponse
            {
                Title = book.Title,
                Author = book.Author,
                Genre = book.Genre
                            .Select(g => (EnumGenre)g.TypeIdentifier)
                            .ToList(),
                Stock = book.Stock,
                Price = book.Price,
            };
        }
    }
}
