using GerenciadorLivraria.Application.Common.Exceptions;
using GerenciadorLivraria.Communication.Requests;
using GerenciadorLivraria.Domain.Entities;
using GerenciadorLivraria.Domain.Repositories;
using GerenciadorLivraria.Domain.Repositories.Book;

namespace GerenciadorLivraria.Application.UseCases.Book.Update
{
    public class UpdateBookUseCase : IUpdateBookUseCase
    {
        private readonly IBookRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        public UpdateBookUseCase(IBookRepository bookRepository, IUnitOfWork unitOfWork)
        {
            _repository = bookRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Execute(UpdateBookRequest request, Guid id)
        {
            Validate(request);

            BookEntity? book = await _repository.GetById(id);

            if (book == null)
                throw new NotFoundException("Livro não encontrado");

            book.Title = request.Title;
            book.Author = request.Author;
            book.Price = request.Price;
            book.Stock = request.Stock;
            book.UpdatedAt = DateTime.Now;

            _repository.Update(book);
            await _unitOfWork.Commit();
        }

        private static void Validate(UpdateBookRequest request)
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
