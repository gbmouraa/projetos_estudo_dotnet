using GerenciadorLivraria.Domain.Entities;

namespace GerenciadorLivraria.Domain.Repositories
{
    public interface IBookRepository
    {
        Task<List<BookEntity>> GetAll();
        Task Add(BookEntity book);
    }
}
