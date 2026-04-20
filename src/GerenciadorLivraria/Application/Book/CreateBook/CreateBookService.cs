using GerenciadorLivraria.Application.Exceptions;
using GerenciadorLivraria.Domain.Entities;
using GerenciadorLivraria.Infrastructure.DataBase;

namespace GerenciadorLivraria.Application.Book.CreateBook
{
    public class CreateBookService : ICreateBookService
    {
        private readonly GerenciadorLivrariaDbContext _dbContext;

        public CreateBookService(GerenciadorLivrariaDbContext database)
        {
            _dbContext = database;
        }

        public CreateBookResult Execute(CreateBookModel model)
        {
            Validate(model);

            BookEntity book = new BookEntity
            {
                Id = new Guid(),
                Title = model.Title,
                Author = model.Author,
                Price = model.Price,
                Stock = model.Stock,
            };

            _dbContext.Add(book);
            _dbContext.SaveChanges();

            return new CreateBookResult { Id = book.Id, Title = book.Title };
        }

        public void Validate(CreateBookModel model)
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
