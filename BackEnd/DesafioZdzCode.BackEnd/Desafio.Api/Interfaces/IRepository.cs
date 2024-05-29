﻿using Desafio.Api.Entities;

namespace Desafio.Api.Interfaces
{
    public interface IRepository<T> where T : Entity
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(Guid id);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
