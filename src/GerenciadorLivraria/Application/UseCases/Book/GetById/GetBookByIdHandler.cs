using GerenciadorLivraria.Application.Common.Exceptions;
using GerenciadorLivraria.Communication.Responses;
using GerenciadorLivraria.Domain.Entities;
using GerenciadorLivraria.Infrastructure.DataBase;
using MediatR;

namespace GerenciadorLivraria.Application.UseCases.Book.GetById
{
    public class GetBookByIdHandler : IRequestHandler<GetBookByIdQuery, BookResponse>
    {
        private readonly GerenciadorLivrariaDbContext _dbContext;

        public GetBookByIdHandler(GerenciadorLivrariaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<BookResponse> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            BookEntity? book = _dbContext.Books.FirstOrDefault(b => b.Id == request.Id);

            if (book == null)
                throw new NotFoundException("Livro não encontrado.");

            return new BookResponse
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                Stock = book.Stock,
                Price = book.Price,
            };
        }
    }
}
