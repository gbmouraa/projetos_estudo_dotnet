using GerenciadorLivraria.Domain.Entities;
using GerenciadorLivraria.Domain.Enums;
using GerenciadorLivraria.Infrastructure.DataBase;
using GerenciadorLivraria.Application.Common.Exceptions;

namespace GerenciadorLivraria.Application.Book.CreateBook
{
    public class CreateBookUseCase
    {
        private readonly GerenciadorLivrariaDbContext _dbContext;

        public CreateBookUseCase(GerenciadorLivrariaDbContext database)
        {
            _dbContext = database;
        }

        public CreateBookResponse Execute(CreateBookRequest request)
        {
            Validate(request);

            var genres = _dbContext.Genres
                                   .Where(g => request.Genre
                                   .Contains((EnumGenre)g.TypeIdentifier))
                                   .ToList();

            // usar mapper
            BookEntity book = new BookEntity
            {
                Id = new Guid(),
                Title = request.Title,
                Author = request.Author,
                Price = request.Price,
                Stock = request.Stock,
                Genre = genres,
                CreatedAt = DateTime.Now,
                UpdatedAt = null
            };

            _dbContext.Add(book);
            _dbContext.SaveChanges();

            return new CreateBookResponse { Id = book.Id, Title = book.Title };
        }

        public void Validate(CreateBookRequest model)
        {
            var validator = new CreateBookValidator();
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
