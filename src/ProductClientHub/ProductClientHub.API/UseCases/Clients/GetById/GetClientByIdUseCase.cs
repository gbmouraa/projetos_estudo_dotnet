using ProductClientHub.API.Infrastructure;
using ProductClientHub.Communication.Responses.Clients;
using ProductClientHub.Exceptions.ExceptionsBase;

namespace ProductClientHub.API.UseCases.Clients.GetById
{
    public class GetClientByIdUseCase
    {
        public ResponseShortClientJson Execute(Guid id)
        {
            var dbContext = new ProductClientHubDbContext();

            var client = dbContext.Clients.FirstOrDefault(c => c.Id == id);

            if (client == null)
            {
                throw new NotFoundException("Usuário não encontrado");
            }

            return new ResponseShortClientJson { Id = client.Id, Name = client.Name };
        }
    }
}
