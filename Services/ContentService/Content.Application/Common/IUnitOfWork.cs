using Content.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Content.Application.Common
{
    public interface IUnitOfWork
    {
        DbSet<TEntity> Set<TEntity>()
            where TEntity : class, IEntity;

        Task SaveAsync(CancellationToken cancellationToken = default);
    }
}