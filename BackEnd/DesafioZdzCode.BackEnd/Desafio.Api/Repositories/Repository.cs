using Desafio.Api.Data;
using Desafio.Api.Entities;
using Desafio.Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Desafio.Api.Repositories
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        private readonly DesafioContext _context;

        public Repository(DesafioContext context)
        {
            _context = context;
        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }
    }
}
