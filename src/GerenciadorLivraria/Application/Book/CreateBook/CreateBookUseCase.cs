using GerenciadorLivraria.Application.Exceptions;
using GerenciadorLivraria.Domain.Entities;
using GerenciadorLivraria.Domain.Enums;
using GerenciadorLivraria.Infrastructure.DataBase;

namespace GerenciadorLivraria.Application.Book.CreateBook
{
    public class CreateBookUseCase
    {
        private readonly GerenciadorLivrariaDbContext _dbContext;

        public CreateBookUseCase(GerenciadorLivrariaDbContext database)
        {
            _dbContext = database;
        }

        public CreateBookResponse Execute(CreateBookRequest model)
        {
            Validate(model);

            var genres = _dbContext.Genres
                .Where(g => model.Genre.Contains((EnumGenre)g.TypeIdentifier))
                .ToList();

            BookEntity book = new BookEntity
            {
                Id = new Guid(),
                Title = model.Title,
                Author = model.Author,
                Price = model.Price,
                Stock = model.Stock,
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
                var errors = result.Errors.Select(x => x.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errors);
            }
        }
    }
}
