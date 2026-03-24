using ProductClientHub.API.Entities;
using ProductClientHub.API.Infrastructure;
using ProductClientHub.API.UseCases.Clients.Validator;
using ProductClientHub.Communication.Requests.Clients;
using ProductClientHub.Communication.Responses.Clients;
using ProductClientHub.Exceptions.ExceptionsBase;

namespace ProductClientHub.API.UseCases.Clients.Register
{
    public class RegisterClientUseCase
    {
        public ResponseShortClientJson Execute(RequestClientJson request)
        {
            Validate(request);

            var dbContext = new ProductClientHubDbContext();

            if (dbContext.Clients.Any(c => c.Email == request.Email))
            {
                throw new EmailInUseException();
            }

            Client client = new Client { Id = new Guid(), Name = request.Name, Email = request.Email };

            dbContext.Clients.Add(client);
            dbContext.SaveChanges();

            return new ResponseShortClientJson { Id = client.Id, Name = client.Name };
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
