﻿using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace login_page.Infrastructure.IRepository
{
    public interface IGenericRepository<T> where T : class
    {

        Task<T> GetAsync(Expression<Func<T, bool>> expression);

        Task<IQueryable<T>> GetAllAsync(Expression<Func<T, bool>> expression = null);

        Task<T> CreateAsync(T entity);

        Task<T> UpdateAsync(T entity);

        Task<bool> DeleteAsync(Expression<Func<T, bool>> expression);
    }
}
