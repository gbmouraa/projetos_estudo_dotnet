using GerenciadorLivraria.Domain.Enums;
using GerenciadorLivraria.Infrastructure.DataBase;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorLivraria.Application.Book.GetAllBooks
{
    public class GetAllBooksUseCase
    {
        private readonly GerenciadorLivrariaDbContext _dbContext;

        public GetAllBooksUseCase(GerenciadorLivrariaDbContext database)
        {
            _dbContext = database;
        }

        public List<BookResponse> Execute()
        {
            List<BookResponse> booksResponse = [];

            var books = _dbContext.Books.Include(b => b.Genre);

            if (books.Any())
            {
                books.ForEachAsync(b => booksResponse.Add(new BookResponse
                {
                    Title = b.Title,
                    Author = b.Author,
                    Genre = b.Genre.Select(g => (EnumGenre)g.TypeIdentifier).ToList()
                }));
            }

            return booksResponse;
        }
    }
}
