using AutoMapper;
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
        private readonly IMapper _mapper;
        public UpdateBookUseCase(IBookRepository bookRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = bookRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task Execute(UpdateBookRequest request, Guid id)
        {
            Validate(request);

            BookEntity? book = await _repository.GetById(id);

            if (book == null)
                throw new NotFoundException("Livro não encontrado");

            _mapper.Map(request, book);
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
