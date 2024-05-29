using Desafio.Api.Data;
using Desafio.Api.Interfaces;

namespace Desafio.Api.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DesafioContext _context;

        private IRepositoryCategory? categoryRepository;
        private IRepositoryProduct? productRepository;

        public UnitOfWork(DesafioContext context)
        {
            _context = context;
        }

        public IRepositoryCategory RepositoryCategory 
        {
            get 
            { 
                return categoryRepository = categoryRepository ?? new RepositoryCategory(_context); 
            } 
        }

        public IRepositoryProduct RepositoryProduct 
        {
            get 
            {  
                return productRepository = productRepository ?? new RepositoryProduct(_context); 
            } 
        }

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }
    }
}
