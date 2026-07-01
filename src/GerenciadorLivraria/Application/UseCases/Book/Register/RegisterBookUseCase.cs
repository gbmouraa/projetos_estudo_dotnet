using AutoMapper;
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
        private readonly IMapper _mapper;

        public RegisterBookUseCase(IBookRepository repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<RegisterBookResponse> Execute(RegisterBookRequest request)
        {
            Validate(request);

            var book = _mapper.Map<BookEntity>(request);

            await _repository.Add(book);
            await _unitOfWork.Commit();

            return _mapper.Map<RegisterBookResponse>(book);
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
