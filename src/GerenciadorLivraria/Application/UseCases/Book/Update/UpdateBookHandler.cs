using GerenciadorLivraria.Application.UseCases.Book.Update;
using GerenciadorLivraria.Application.Common.Exceptions;
using GerenciadorLivraria.Domain.Entities;
using GerenciadorLivraria.Infrastructure.DataBase;
using MediatR;

namespace GerenciadorLivraria.Application.UseCases.Book.Update
{
    public class UpdateBookHandler : IRequestHandler<UpdateBookCommand, Unit>
    {
        private readonly GerenciadorLivrariaDbContext _dbContext;

        public UpdateBookHandler(GerenciadorLivrariaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            Validate(request);

            var id = new Guid();

            BookEntity? book = _dbContext.Books.FirstOrDefault(b => b.Id == id);

            if (book == null)
                throw new NotFoundException("Livro não encontrado");

            book.Title = request.Title;
            book.Author = request.Author;
            book.Price = request.Price;
            book.Stock = request.Stock;
            book.UpdatedAt = DateTime.Now;

            _dbContext.Update(book);
            await _dbContext.SaveChangesAsync();

            return Unit.Value;
        }

        private static void Validate(UpdateBookCommand request)
        {
            var validator = new UpdateBookValidator();
            var result = validator.Validate(request);

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
