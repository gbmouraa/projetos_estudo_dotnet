using FluentValidation;

namespace GerenciadorLivraria.Application.Book.CreateBook
{
    public class CreateBookValidator : AbstractValidator<CreateBookModel>
    {
        public CreateBookValidator()
        {
            RuleFor(book => book.Title).NotEmpty().WithMessage("O titulo do livro não pode ser vazio.");
            RuleFor(book => book.Author).NotEmpty().WithMessage("O nome do autor não pode ser vazio.");
            RuleFor(book => book.Genre).NotEmpty().WithMessage("Adicione pelo menos um genero para o livro.");
            RuleForEach(book => book.Genre).IsInEnum().WithMessage("O gênero '{PropertyValue}' é inválido.");
            RuleFor(book => book.Price).GreaterThan(0).WithMessage("Insira um preço válido.");
            RuleFor(book => book.Price).GreaterThanOrEqualTo(0).WithMessage("Estoque deve ser maior que zero.");
        }
    }
}
