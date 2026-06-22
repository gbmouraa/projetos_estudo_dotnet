using GerenciadorLivraria.Application.UseCases.Book.Register;
using GerenciadorLivraria.Application.Common.Exceptions;
using GerenciadorLivraria.Communication.Responses;
using GerenciadorLivraria.Domain.Entities;
using GerenciadorLivraria.Infrastructure.DataBase;
using MediatR;

namespace GerenciadorLivraria.Application.UseCases.Book.Register
{
    public class RegisterBookHandler : IRequestHandler<RegisterBookCommand, RegisterBookResponse>
    {
        private readonly GerenciadorLivrariaDbContext _dbContext;

        public RegisterBookHandler(GerenciadorLivrariaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<RegisterBookResponse> Handle(RegisterBookCommand request, CancellationToken cancellationToken)
        {
            Validate(request);

            BookEntity book = new BookEntity
            {
                Id = new Guid(),
                Title = request.Title,
                Author = request.Author,
                Price = request.Price,
                Stock = request.Stock,
                CreatedAt = DateTime.Now,
            };

            _dbContext.Add(book);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return new RegisterBookResponse { Id = book.Id, Title = book.Title };
        }

        public void Validate(RegisterBookCommand request)
        {
            var validator = new RegisterBookValidator();
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
