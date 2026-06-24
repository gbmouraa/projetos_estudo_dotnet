namespace GerenciadorLivraria.Domain.Repositories
{
    public interface IUnitOfWork
    {
        Task Commit();
    }
}
