﻿using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace BuildingBlocks.Repository.Service
{
    public interface IRepositoryAsync<T> where T : class
    {
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>>? predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
            bool disableTracking = false);

        IQueryable<T> Query(Expression<Func<T, bool>>? where = null,
                        Func<IQueryable<T>,
                        IIncludableQueryable<T, object>>? include = null,
                        bool disableTracking = false, string fieldsToSelect = "");
        IQueryable<T> GetAsync(Expression<Func<T, bool>>? where = null);

        IQueryable<T> GetAsync(Func<IQueryable<T>, IIncludableQueryable<T, object>> include, Expression<Func<T, bool>>? where = null);

        Task<IQueryable<T>> ExecuteQuery([NotParameterized] string sql, params object[] parameters);
        ValueTask<EntityEntry<T>> AddAsync(T entity, CancellationToken cancellationToken = default(CancellationToken));

        Task AddAsync(params T[] entities);

        Task AddAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default(CancellationToken));

        void UpdateAsync(T entity);

        void Delete(object id);

        void Delete(IEnumerable<T> entities);

        //Task BulkInsertAsync(IList<T> entities);
        //Task BulkUpdateAsync(IList<T> entities);
        //Task BulkDeleteAsync(IList<T> entities);
        //Task BulkInsertOrUpdateAsync(IList<T> entities);
    }
}
