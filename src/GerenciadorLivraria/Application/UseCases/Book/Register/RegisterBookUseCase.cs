using GerenciadorLivraria.Application.Common.Exceptions;
using GerenciadorLivraria.Communication.Requests;
using GerenciadorLivraria.Communication.Responses;
using GerenciadorLivraria.Domain.Entities;
using GerenciadorLivraria.Domain.Repositories;
using GerenciadorLivraria.Domain.Repositories.Book;

namespace GerenciadorLivraria.Application.UseCases.Book.Register
{
    public class RegisterBookUseCase : IRegisterBookUseCase
    {
        private readonly IBookRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public RegisterBookUseCase(IBookRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
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
            await _unitOfWork.Commit();

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
