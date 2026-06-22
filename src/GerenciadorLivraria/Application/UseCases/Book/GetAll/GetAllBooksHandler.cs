using GerenciadorLivraria.Communication.Responses;
using GerenciadorLivraria.Infrastructure.DataBase;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorLivraria.Application.UseCases.Book.GetAll
{
    public class GetAllBooksHandler : IRequestHandler<GetAllBooksQuery, List<BookResponse>>
    {
        private readonly GerenciadorLivrariaDbContext _dbContext;

        public GetAllBooksHandler(GerenciadorLivrariaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<BookResponse>> Handle(GetAllBooksQuery query, CancellationToken cancellationToken)
        {
            return await _dbContext.Books
                                   .AsNoTracking()
                                   .Select(b => new BookResponse
                                   {
                                       Id = b.Id,
                                       Author = b.Author,
                                       Title = b.Title,
                                       Stock = b.Stock,
                                       Price = b.Price,
                                   })
                                   .ToListAsync(cancellationToken);
        }
    }
}
