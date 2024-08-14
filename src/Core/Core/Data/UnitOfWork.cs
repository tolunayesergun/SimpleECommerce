using Core.Data.Abstracts;
using Microsoft.EntityFrameworkCore;

namespace Core.Data
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private IDbContext _dbContext;

        private Dictionary<Type, object> repos;

        public UnitOfWork(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Commit()
        {
            return _dbContext.SaveChanges();
        }

        public async Task<int> CommitAsync()
        {
            return await _dbContext.SaveChangesAsync(CancellationToken.None);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(obj: this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_dbContext != null)
                {
                    _dbContext.Dispose();
                    _dbContext = null;
                }
            }
        }
    }
}