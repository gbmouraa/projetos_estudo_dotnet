using ProductClientHub.API.Infrastructure;
using ProductClientHub.API.UseCases.Clients.Validator;
using ProductClientHub.Communication.Requests.Clients;
using ProductClientHub.Exceptions.ExceptionsBase;

namespace ProductClientHub.API.UseCases.Clients.Update
{
    public class UpdateClientUseCase
    {
        public void Execute(Guid id, RequestClientJson request)
        {
            Validate(request);

            var dbContext = new ProductClientHubDbContext();

            var client = dbContext.Clients.FirstOrDefault(c => c.Id == id);

            if (client == null)
            {
                throw new NotFoundException("Usuário não encontrado");
            }

            client.Name = request.Name;
            client.Email = request.Email;

            dbContext.Update(client);
            dbContext.SaveChanges();
        }


        public void Validate(RequestClientJson request)
        {
            var validator = new ClientValidator();

            var result = validator.Validate(request);

            if (!result.IsValid)
            {
                var errors = result.Errors.Select(x => x.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errors);
            }
        }
    }
}
