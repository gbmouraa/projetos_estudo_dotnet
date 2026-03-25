using ProductClientHub.API.Infrastructure;
using ProductClientHub.Exceptions.ExceptionsBase;

namespace ProductClientHub.API.UseCases.Clients.Delete
{
    public class DeleteClientUseCase
    {
        public void Execute(Guid id)
        {
            var dbContext = new ProductClientHubDbContext();

            var client = dbContext.Clients.FirstOrDefault(c => c.Id == id);

            if (client == null)
            {
                throw new NotFoundException("Usuário não encontrado");
            }

            dbContext.Clients.Remove(client);
            dbContext.SaveChanges();
        }
    }
}
