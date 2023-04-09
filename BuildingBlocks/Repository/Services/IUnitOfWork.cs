using Microsoft.EntityFrameworkCore;

namespace BuildingBlocks.Repository.Service;

public interface IUnitOfWork : IDisposable
{
    IRepositoryAsync<TEntity> GetRepositoryAsync<TEntity>() where TEntity : class;
    int SaveChanges();
}

public interface IUnitOfWork<out TContext> : IUnitOfWork where TContext : DbContext
{
    TContext Context { get; }
}
