using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.Repository.Extensions;

public static class ServiceExtensions
{
    public static void AddDbContextProvider<TDbContext>(this IServiceCollection services, string connection) where TDbContext : DbContext
    {
        services.AddDbContext<TDbContext>(o => o.UseSqlServer(connection)).AddUnitOfWork<TDbContext>();//Need to extens this to register dispatch event handler
    }
}
