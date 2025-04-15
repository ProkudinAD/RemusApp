using Content.Application.Common;
using Microsoft.EntityFrameworkCore;

namespace Content.Infrastructure
{
    public sealed class UnitOfWork(ContentDbContext dbContext) : IUnitOfWork
    {
        DbSet<TEntity> IUnitOfWork.Set<TEntity>()
        {
            return dbContext.Set<TEntity>();
        }

        public async Task SaveAsync(CancellationToken cancellationToken)
        {
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}