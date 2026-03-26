using Microsoft.EntityFrameworkCore;
using ProductClientHub.API.Infrastructure;
using ProductClientHub.Communication.Responses.Clients;
using ProductClientHub.Communication.Responses.Products;
using ProductClientHub.Exceptions.ExceptionsBase;

namespace ProductClientHub.API.UseCases.Clients.GetById
{
    public class GetClientByIdUseCase
    {
        public ResponseClientJson Execute(Guid id)
        {
            var dbContext = new ProductClientHubDbContext();

            var client = dbContext.Clients.Include(c => c.Products).FirstOrDefault(c => c.Id == id);

            if (client == null)
            {
                throw new NotFoundException("Usuário não encontrado");
            }

            return new ResponseClientJson
            {
                Id = client.Id,
                Name = client.Name,
                Products = client.Products.Select(p => new ResponseShortProductJson { Id = p.Id, Name = p.Name }).ToList()
            };
        }
    }
}
