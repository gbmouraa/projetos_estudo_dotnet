namespace GerenciadorLivraria.Application.UseCases.Book.Delete
{
    public interface IDeleteBookUseCase
    {
        Task Execute(Guid id);
    }
}
