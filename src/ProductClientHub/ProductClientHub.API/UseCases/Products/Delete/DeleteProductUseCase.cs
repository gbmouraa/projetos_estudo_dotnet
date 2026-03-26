using ProductClientHub.API.Infrastructure;
using ProductClientHub.Exceptions.ExceptionsBase;

namespace ProductClientHub.API.UseCases.Products.Delete
{
    public class DeleteProductUseCase
    {
        public void Execute(Guid productId)
        {
            var dbContext = new ProductClientHubDbContext();

            var product = dbContext.Products.FirstOrDefault(x => x.Id == productId);

            if (product == null)
            {
                throw new NotFoundException("Produto nao encontrado");
            }

            dbContext.Products.Remove(product);
            dbContext.SaveChanges();
        }
    }
}
