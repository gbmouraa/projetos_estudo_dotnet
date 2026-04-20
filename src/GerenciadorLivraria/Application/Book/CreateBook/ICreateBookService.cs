namespace GerenciadorLivraria.Application.Book.CreateBook
{
    public interface ICreateBookService
    {
        public CreateBookResult Execute(CreateBookModel model);
    }
}
