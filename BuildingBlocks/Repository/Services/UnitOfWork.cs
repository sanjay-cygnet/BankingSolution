using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Repository.Service
{
    public class UnitOfWork<TContext> : IUnitOfWork<TContext> where TContext : DbContext, IDisposable
    {
        private Dictionary<Type, object>? _repositories;
        private readonly ILogger<DbContext> _logger;

        public UnitOfWork(TContext context,
            IConfiguration configuration,
            ILogger<DbContext> logger)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
            _configuration = configuration;
            _logger = logger;
        }

        public IRepositoryAsync<TEntity> GetRepositoryAsync<TEntity>() where TEntity : class
        {
            if (_repositories == null) _repositories = new Dictionary<Type, object>();
            var type = typeof(TEntity);
            if (!_repositories.ContainsKey(type)) _repositories[type] = new RepositoryAsync<TEntity>(Context, _configuration);
            return (IRepositoryAsync<TEntity>)_repositories[type];
        }
        public TContext Context { get; }

        private readonly IConfiguration _configuration;

        public int SaveChanges()
        {
            return Context.SaveChanges();
        }

        public void Dispose()
        {
            Context?.Dispose();
        }
    }
}
