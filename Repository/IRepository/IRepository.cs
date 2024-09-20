﻿using System.Linq.Expressions;

namespace BetaLogistics.Repository.IRepository
{
    public interface IRepository<T> where T : class
    { 
        Task<T>GetAsync(Expression<Func<T, bool>> filter = null, bool tracked = true, string? includeProperties = null);
        Task<List<T>> GetAllAsync(Expression<Func<T,bool>> filter = null, bool tracked = true, string? includeProperties = null,
            int pageSize=3, int pageNumber=1);
        Task CreateAsync(T entity);
        // Task UpdateAsync(T entity);
        Task RemoveAsync(T entity);
        Task SaveAsync();
    }
}
