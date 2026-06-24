using GerenciadorLivraria.Domain.Repositories;

namespace GerenciadorLivraria.Infrastructure.DataBase
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly GerenciadorLivrariaDbContext _dbContext;
        public UnitOfWork(GerenciadorLivrariaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Commit()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
