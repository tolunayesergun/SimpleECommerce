using Core.Data.Abstracts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Core.Data
{
    public class GenericRepository<T> : IGenericRepository<T>
             where T : class
    {
        private readonly IDbContext _context;
        private readonly DbSet<T> dbSet;

        public GenericRepository(IDbContext context)
        {
            _context = context;
            dbSet = context.Set<T>();
        }

        public virtual EntityState Add(T entity)
        {
            return dbSet.Add(entity).State;
        }

        public bool Exists(Expression<Func<T, bool>> predicate)
        {
            return dbSet.Any(predicate);
        }

        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return dbSet.Where(predicate);
        }

        public IQueryable<T> GetAll()
        {
            return dbSet;
        }

        public async Task<T?> GetAsync(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public virtual EntityState Update(T entity)
        {
            return dbSet.Update(entity).State;
        }
    }
}