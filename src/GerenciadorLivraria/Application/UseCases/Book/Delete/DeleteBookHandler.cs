using GerenciadorLivraria.Application.Common.Exceptions;
using GerenciadorLivraria.Domain.Entities;
using GerenciadorLivraria.Infrastructure.DataBase;
using MediatR;

namespace GerenciadorLivraria.Application.UseCases.Book.Delete
{
    public class DeleteBookHandler : IRequestHandler<DeleteBookCommand, Unit>
    {
        private readonly GerenciadorLivrariaDbContext _dbContext;

        public DeleteBookHandler(GerenciadorLivrariaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            BookEntity? book = _dbContext.Books.FirstOrDefault(b => b.Id == request.Id);

            if (book == null)
                throw new NotFoundException("Livro não encontrado.");

            _dbContext.Remove(book);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
