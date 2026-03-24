using FluentValidation;
using ProductClientHub.Communication.Requests.Clients;

namespace ProductClientHub.API.UseCases.Clients.Validator
{
    public class ClientValidator : AbstractValidator<RequestClientJson>
    {
        public ClientValidator()
        {
            RuleFor(client => client.Name).NotEmpty().WithMessage("O nome nao pode ser vazio");
            RuleFor(client => client.Email).EmailAddress().WithMessage("O e-mail nao eh valido");
        }
    }
}
