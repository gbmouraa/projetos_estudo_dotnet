using FluentValidation;
using ProductClientHub.Communication.Requests.Products;

namespace ProductClientHub.API.UseCases.Products.Validator
{
    public class ProductValidator : AbstractValidator<RequestProductJson>
    {
        public ProductValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("O nome do produto nao eh valido");
            RuleFor(p => p.Brand).NotEmpty().WithMessage("O marca do produto nao eh valida");
            RuleFor(p => p.Price).GreaterThan(0).WithMessage("O preco do produto nao eh valido");
        }
    }
}
