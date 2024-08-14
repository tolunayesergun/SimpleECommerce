using Microsoft.EntityFrameworkCore;

namespace Core.Data.Abstracts
{
    public interface IDbContext
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        Task<int> SaveChangesAsync(bool confirmAllTransactions, CancellationToken cancellationToken);
        int SaveChanges();
        int SaveChanges(bool confirmAllTransactions);
        void Dispose();
    }
}
