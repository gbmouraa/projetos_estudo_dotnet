using ProductClientHub.API.Entities;
using ProductClientHub.API.Infrastructure;
using ProductClientHub.API.UseCases.Products.Validator;
using ProductClientHub.Communication.Requests.Products;
using ProductClientHub.Communication.Responses.Products;
using ProductClientHub.Exceptions.ExceptionsBase;

namespace ProductClientHub.API.UseCases.Products.Register
{
    public class RegisterProductUseCase
    {
        public ResponseShortProductJson Execute(Guid clientId, RequestProductJson request)
        {
            Validate(clientId, request);

            var dbContext = new ProductClientHubDbContext();

            var product = new Product
            {
                Name = request.Name,
                Brand = request.Brand,
                Price = request.Price,
                ClientId = clientId,
            };

            dbContext.Products.Add(product);
            dbContext.SaveChanges();

            return new ResponseShortProductJson { Id = product.Id, Name = product.Name };
        }

        private static void Validate(Guid clientId, RequestProductJson request)
        {
            var dbContext = new ProductClientHubDbContext();
            var client = dbContext.Clients.FirstOrDefault(c => c.Id == clientId);

            if (client == null)
            {
                throw new NotFoundException("Cliente nao encontrado");
            }

            var validator = new ProductValidator();
            var result = validator.Validate(request);

            if (!result.IsValid)
            {
                var errors = result.Errors.Select(x => x.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errors);
            }
        }
    }
}
