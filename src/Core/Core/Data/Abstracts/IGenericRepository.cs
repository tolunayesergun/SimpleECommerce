using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Abstracts
{
    public interface IGenericRepository<T>
    {
        EntityState Add(T entity);
        EntityState Update(T entity);

        Task<T> GetAsync(int id);

        IQueryable<T?> FindBy(Expression<Func<T, bool>> predicate);
        IQueryable<T> GetAll();

        bool Exists(Expression<Func<T, bool>> predicate);
    }
}
