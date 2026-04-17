using GerenciadorLivraria.Application.Exceptions;

namespace GerenciadorLivraria.Application.Book.CreateBook
{
    public class CreateBookService
    {
        public CreateBookResult Execute(CreateBookModel model)
        {
            Validate(model);

            // dbContext
            // var bookEntity

            return new CreateBookResult { Id = 1, Title = model.Title };
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
