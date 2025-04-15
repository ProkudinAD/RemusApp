using Content.Domain.Entities.News;
using Microsoft.EntityFrameworkCore;

namespace Content.Infrastructure;

public class ContentDbContext : DbContext
{
    public DbSet<NewsMain> NewsMain { get; set; } = null!;

    public ContentDbContext(DbContextOptions<ContentDbContext> options)
        : base(options) { }

    public void BeginTransaction()
    {
        Database.BeginTransaction();
    }

    public void CommitTransaction()
    {
        Database.CommitTransaction();
    }

    public void RollbackTransaction()
    {
        Database.RollbackTransaction();
    }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return base.SaveChangesAsync(cancellationToken);
    }
}