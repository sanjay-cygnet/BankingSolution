using BuildingBlocks.Repository.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.Repository.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddDbContextProvider<TDbContext>(this IServiceCollection services, string connection) where TDbContext : DbContext
        {
            services.AddDbContext<TDbContext>(o => o.UseSqlServer(connection)).AddUnitOfWork<TDbContext>();//Need to extens this to register dispatch event handler


            ///Following code is to choose between data providers like sql, pgsql etc.
            //var provider = configuration.GetCurrentProvider();//like Sql/Pgsql
            //switch (provider)
            //{
            //    case DbProviderEnum.Sql:
            //        services.AddDbContext<TDbContext>(o => o.UseSqlServer(connection)).AddUnitOfWork<TDbContext>(configuration);
            //        break;
            //    case DbProviderEnum.Pgsql:
            //        services.AddDbContext<TDbContext>(o => o.UseNpgsql(connection)).AddUnitOfWork<TDbContext>(configuration);
            //        break;
            //    default:
            //        break;
            //}
        }
    }
}
