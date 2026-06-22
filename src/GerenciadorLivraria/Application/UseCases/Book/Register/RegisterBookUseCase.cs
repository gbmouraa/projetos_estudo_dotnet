using GerenciadorLivraria.Application.Common.Exceptions;
using GerenciadorLivraria.Communication.Requests;
using GerenciadorLivraria.Communication.Responses;
using GerenciadorLivraria.Domain.Entities;
using GerenciadorLivraria.Domain.Repositories;

namespace GerenciadorLivraria.Application.UseCases.Book.Register
{
    public class RegisterBookUseCase : IRegisterBookUseCase
    {
        private readonly IBookRepository _repository;

        public RegisterBookUseCase(IBookRepository repository)
        {
            _repository = repository;
        }

        public async Task<RegisterBookResponse> Execute(RegisterBookRequest request)
        {
            Validate(request);

            BookEntity book = new BookEntity // mapper
            {
                Id = new Guid(),
                Title = request.Title,
                Author = request.Author,
                Price = request.Price,
                Stock = request.Stock,
                CreatedAt = DateTime.Now,
            };

            await _repository.Add(book);
            return new RegisterBookResponse { Id = book.Id, Title = book.Title };
        }

        public void Validate(RegisterBookRequest request)
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
