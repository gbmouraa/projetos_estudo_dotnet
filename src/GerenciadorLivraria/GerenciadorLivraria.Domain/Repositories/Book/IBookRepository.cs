using GerenciadorLivraria.Domain.Entities;

namespace GerenciadorLivraria.Domain.Repositories.Book
{
    public interface IBookRepository
    {
        Task<List<BookEntity>> GetAll();
        Task<BookEntity?> GetById(Guid id);
        Task Add(BookEntity book);
        void Update(BookEntity book);
    }
}
