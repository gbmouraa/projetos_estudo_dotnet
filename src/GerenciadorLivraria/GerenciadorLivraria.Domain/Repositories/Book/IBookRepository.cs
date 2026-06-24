using GerenciadorLivraria.Domain.Entities;

namespace GerenciadorLivraria.Domain.Repositories.Book
{
    public interface IBookRepository
    {
        Task<List<BookEntity>> GetAll();
        Task Add(BookEntity book);
    }
}
