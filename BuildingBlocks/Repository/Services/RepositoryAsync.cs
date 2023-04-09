
#nullable disable
namespace BuildingBlocks.Repository.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Configuration;
using System.Linq.Expressions;

public class RepositoryAsync<T> : IRepositoryAsync<T> where T : class
{
    protected readonly DbContext _dbContext;
    protected readonly DbSet<T> _dbSet;
    private readonly IConfiguration _configuration;

    public RepositoryAsync(DbContext dbContext, IConfiguration configuration)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<T>();
        _configuration = configuration;
    }

    public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>>? predicate = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, bool enableTracking = false)
    {
        IQueryable<T> query = _dbSet;
        if (enableTracking) query = query.AsNoTracking();

        if (include != null) query = include(query);

        if (predicate != null) query = query.Where(predicate);

        if (orderBy != null)
            return await orderBy(query).FirstOrDefaultAsync();
        return await query.FirstOrDefaultAsync();
    }

    public IQueryable<T> Query(Expression<Func<T, bool>>? where = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
        bool enableTracking = false, string fieldsToSelect = "")
    {
        IQueryable<T> query = _dbSet;
        if (enableTracking) query = query.AsNoTracking();

        if (include != null) query = include(query);

        return query.Where(where ?? (e => true));
    }

    public IQueryable<T> GetAsync(Expression<Func<T, bool>>? where)
    {
        return _dbSet.Where(where ?? (e => true));
    }

    public IQueryable<T> GetAsync(Func<IQueryable<T>, IIncludableQueryable<T, object>>? include, Expression<Func<T, bool>>? where)
    {
        IQueryable<T> query = _dbSet;

        if (include != null) query = include(query);

        if (where != null) query = query.Where(where);

        return query;
    }
    public async Task<IQueryable<T>> ExecuteQuery([NotParameterized] string sql, params object[] parameters)
    {
        return await Task.Run(() =>
        {
            return _dbSet.FromSqlRaw<T>(sql, parameters);
        });
    }

    public ValueTask<EntityEntry<T>> AddAsync(T entity, CancellationToken cancellationToken = default(CancellationToken))
    {
        return _dbSet.AddAsync(entity, cancellationToken);
    }

    public Task AddAsync(params T[] entities)
    {
        return _dbSet.AddRangeAsync(entities);
    }

    public Task AddAsync(IEnumerable<T> entities,
        CancellationToken cancellationToken = default(CancellationToken))
    {
        return _dbSet.AddRangeAsync(entities, cancellationToken);
    }

    public void UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
    }

    public ValueTask<EntityEntry<T>> AddAsync(T entity)
    {
        return AddAsync(entity, new CancellationToken());
    }

    public virtual void Delete(IEnumerable<T> entities)
    {
        _dbSet.RemoveRange(entities);
    }

    public virtual void Delete(object id)
    {
        var entityToDelete = _dbSet.Find(id);
        if (entityToDelete != null)
            _dbSet.Remove(entityToDelete);
    }
}
